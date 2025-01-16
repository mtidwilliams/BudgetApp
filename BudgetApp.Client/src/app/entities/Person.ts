import { Address } from "./Address";
import { User } from "./User";

export class Person {
  personId: string;
  firstName: string;
  lastName: string;
  address: Address;
  user: User;

  constructor(personId: string, firstName: string, lastName: string, address: Address, user: User) {
    this.personId = personId;
    this.firstName = firstName;
    this.lastName = lastName;
    this.address = address;
    this.user = user;
  }
}
