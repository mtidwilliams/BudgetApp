
export class Address {
  addressId: string;
  street: string;
  city: string;
  state: string;
  zipCode: number;

  constructor(addressId: string, street: string, city: string, state: string, zipCode: number) {
    this.addressId = addressId;
    this.street = street;
    this.city = city;
    this.state = state;
    this.zipCode = zipCode;
  }
}
