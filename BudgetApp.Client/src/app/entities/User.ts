import { Budget } from "./Budget";
import { Person } from "./Person";

export class User {
  userId: string;
  email: string;
  password: string;
  person: Person;
  budget: Budget;

  constructor(userId: string, email: string, password: string, person: Person, budget: Budget) {
    this.userId = userId;
    this.email = email;
    this.password = password;
    this.person = person;
    this.budget = budget;
  }
}
