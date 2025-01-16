export class Expense {
  expenseId: string;
  name: string;
  amount: number;

  constructor(expenseId: string, name: string, amount: number) {
    this.expenseId = expenseId;
    this.name = name;
    this.amount = amount;
  }
}
