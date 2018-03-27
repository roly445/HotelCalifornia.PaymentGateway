declare interface PaymentRequest {
    canMakePayment():Promise<boolean|any>
}