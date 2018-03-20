export interface PaymentClient {
    onShippingAddressChange(shippingAddressJson:string): Promise<PaymentShippingOption[]>;
}