export interface PaymentResult {
    success: boolean;
    statusCode: number;
    statusMessage: string;
    methodName: string;
    payerEmail?: string;
    payerName?: string;
    payerPhone?: string;
    shippingAddress?: PaymentAddress;
    shippingOption?: string;
}