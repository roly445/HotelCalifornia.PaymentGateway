export interface Integration {
    schemes: string[];
    currency: string;
    identifyingToken: string
    methods: string[];
    cardPaymentEnabled: boolean;
    cardPostUrl: string;
}