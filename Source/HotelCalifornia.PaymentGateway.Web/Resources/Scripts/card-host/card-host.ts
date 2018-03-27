import Penpal from 'penpal'
import { PaymentResult } from './data/payment-result'
import { ShippingInfo } from './data/shipping-info'
import { AdditionalOptions } from './data/addition-options'
import { GatewayResponse } from './data/gateway-response';
import { PaymentClient } from './data/payment-client';
import { PaymentInfo } from './data/payment-info';
import { Integration } from './data/integration';

export class Host {
    connectionPromise: Promise<PaymentClient>;
    originalTotal: PaymentItem;
    shippingInfo: ShippingInfo;
    paymentShippingOptions: PaymentShippingOption[];
    paymentRequest: PaymentRequest;
    private cardPaymentLiteral:string = '%%__cardPaymentEnabled__%%';
    private integration:Integration = {
        schemes: ['%%__schemes__%%'],
        cardPaymentEnabled: this.cardPaymentLiteral === 'True',
        cardPostUrl: '%%__cardPostUrl__%%',
        currency: '%%__currency__%%',
        identifyingToken: '%%__identifyingToken__%%',
        methods: ['%%__methods__%%']
    }

    constructor() {

        var contextThis = this;
        var clientConnection = Penpal.connectToParent({
            methods: {
                start(paymentInfo:PaymentInfo, shippingInfo?:ShippingInfo, additionalOptions?:AdditionalOptions): Promise<PaymentResult> {

                    const supportedPaymentMethods:PaymentMethodData[] = [{
                        supportedMethods: 'basic-card',
                        data: {
                            supportedNetworks: contextThis.integration.schemes,
                            supportedMethods: contextThis.integration.methods
                          },
                    }];
                    contextThis.originalTotal = {
                        label: paymentInfo.totalLabel ? paymentInfo.totalLabel : 'Total',
                        amount:{
                            currency: contextThis.integration.currency,
                            value: paymentInfo.amount.toString()
                        }
                    }
                    let paymentDetails:PaymentDetails = {
                        total: contextThis.originalTotal
                    };
                    let options: PaymentOptions = {};

                    if (shippingInfo) {
                        options.requestShipping = true;
                        if(shippingInfo.useSubTotal) {
                            paymentDetails.displayItems = [];
                            paymentDetails.displayItems.push({
                                label:  shippingInfo.subTotalLabel ? shippingInfo.subTotalLabel : 'Sub Total',
                                amount: {
                                    value: contextThis.originalTotal.amount.value,
                                    currency:  contextThis.integration.currency
                                }
                            })
                            paymentDetails.displayItems.push({
                                label: shippingInfo.deliveryLabel ? shippingInfo.deliveryLabel : 'Delivery',
                                amount: {
                                    value: '0',
                                    currency:  contextThis.integration.currency
                                }
                            })
                        }
                    }
                    if (additionalOptions) {
                        options.requestPayerEmail = additionalOptions.requestPayerEmail;
                        options.requestPayerName = additionalOptions.requestPayerName;    
                        options.requestPayerPhone = additionalOptions.requestPayerPhone;
                    }

                    contextThis.paymentRequest = new PaymentRequest(
                        supportedPaymentMethods,
                        paymentDetails,
                        options
                    );
                    contextThis.paymentRequest.canMakePayment().then((a:boolean)  => {

                    })

                    if (shippingInfo) {
                        contextThis.shippingInfo = shippingInfo;
                        contextThis.paymentRequest.addEventListener('shippingaddresschange', (event:PaymentRequestUpdateEvent) => { 
                            contextThis.onShippingAddressChange(event);
                        });
                        contextThis.paymentRequest.addEventListener('shippingoptionchange', (event:PaymentRequestUpdateEvent) => {
                            contextThis.onShippingOptionChange(event);
                        });    
                    }


                    return contextThis.paymentRequest
                        .show()
                    
            .then((paymentResponse: PaymentResponse) => {
                return fetch(contextThis.integration.cardPostUrl, {
                    method: 'post',
                    body: JSON.stringify({
                        identifingToken: contextThis.integration.identifyingToken,
                        details: paymentResponse.details
                    }),
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                })
                .then((serverResponse: Response) => {
                    return serverResponse.json()
                    .then((gatewayResponse:GatewayResponse) => {
                        if (gatewayResponse.success) {
                            return paymentResponse.complete('success')
                            .then(value => {
                                var data:PaymentResult = {
                                    success: gatewayResponse.success,
                                    statusCode: gatewayResponse.statusCode,
                                    statusMessage: gatewayResponse.statusMessage,
                                    methodName: paymentResponse.methodName,
                                    payerEmail: paymentResponse.payerEmail,
                                    payerName: paymentResponse.payerName,
                                    payerPhone: paymentResponse.payerPhone,
                                    shippingAddress: JSON.stringify(paymentResponse.shippingAddress),
                                    shippingOption: paymentResponse.shippingOption
                                };
                                return data;
                            });
                        } else {
                            return paymentResponse.complete('success')
                            .then(value => {
                                var data:PaymentResult = {
                                    success: gatewayResponse.success,
                                    statusCode: gatewayResponse.statusCode,
                                    statusMessage: gatewayResponse.statusMessage,
                                    methodName: paymentResponse.methodName
                                };
                                return data;
                            });
                        }
                    })
                })
                        }).catch(err => {
                            console.log(err);
                            var data: PaymentResult = {
                                success: false,
                                statusCode: 99,
                                statusMessage: 'unknown',
                                methodName: 'card'
                            };
                            return data;
                        })
                    
                },
                abort() {
                    contextThis.paymentRequest.abort();
                }
            }
        })
        this.connectionPromise = clientConnection.promise;
    }

    private onShippingAddressChange(event:PaymentRequestUpdateEvent) {
        const paymentRequest:PaymentRequest = event.target as PaymentRequest;
        const contextThis = this;
        event.updateWith(
            contextThis.connectionPromise.then((client: PaymentClient) => {
                let shippingAddressJson = JSON.stringify(paymentRequest.shippingAddress);
                return client.onShippingAddressChange(shippingAddressJson)
                    .then((paymentShippingOptions: PaymentShippingOption[]) => {
                        contextThis.paymentShippingOptions = paymentShippingOptions;

                        let paymentItems:PaymentItem[] = [];
                        if(contextThis.shippingInfo.useSubTotal) {
                            paymentItems.push({
                              label:  contextThis.shippingInfo.subTotalLabel ? contextThis.shippingInfo.subTotalLabel : 'Sub Total',
                              amount: {
                                  value: this.originalTotal.amount.value,
                                  currency:  this.integration.currency
                              }
                          });
                          paymentItems.push({
                                      label: contextThis.shippingInfo.deliveryLabel ? contextThis.shippingInfo.deliveryLabel : 'Delivery',
                                      amount: {
                                          value: '0',
                                          currency:  this.integration.currency
                                      }
                                  })     
                        }
                        return {
                            total: contextThis.originalTotal,
                            displayItems: paymentItems,
                            shippingOptions: paymentShippingOptions
                        };
                    });
                    
                })
            );
    }

    private onShippingOptionChange(event:PaymentRequestUpdateEvent) {
        const paymentRequest:PaymentRequest = event.target as PaymentRequest;
        const contextThis = this;
        const selectedId = paymentRequest.shippingOption;
        
        let shippingCost:number = 0;
        contextThis.paymentShippingOptions.forEach((option: any) => {
            if (option.id === selectedId) {
                option.selected = true;
                shippingCost = parseFloat(option.amount.value);
            } else {
                option.selected = false;
            }            
        });
      
        var newTotal = JSON.parse(JSON.stringify(this.originalTotal));
        newTotal.amount.value = (parseFloat(newTotal.amount.value) + shippingCost).toString();
        let paymentItems:PaymentItem[] = [];
                    if(contextThis.shippingInfo.useSubTotal) {
                        
                        paymentItems.push({
                            label:  contextThis.shippingInfo.subTotalLabel ? contextThis.shippingInfo.subTotalLabel : 'Sub Total',
                            amount: {
                                value: this.originalTotal.amount.value,
                                currency:  this.integration.currency
                            }
                        })
                        paymentItems.push({
                            label: contextThis.shippingInfo.deliveryLabel ? contextThis.shippingInfo.deliveryLabel : 'Delivery',
                            amount: {
                                value: shippingCost.toString(),
                                currency:  this.integration.currency
                            }
                        })
                    }

        event.updateWith(Promise.resolve( {
           total: newTotal,
           displayItems: paymentItems,
           shippingOptions: contextThis.paymentShippingOptions
        }));
    }
}

if (document.readyState !== 'loading') {
    new Host();
} else {
    document.addEventListener('DOMContentLoaded', e => new Host());
} 

