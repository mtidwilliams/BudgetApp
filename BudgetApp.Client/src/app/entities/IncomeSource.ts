export class IncomeSource {
  incomeSourceId: string;
  name: string;
  amount: number;

  constructor(incomeSourceId: string, name: string, amount: number) {
    this.incomeSourceId = incomeSourceId;
    this.name = name;
    this.amount = amount;
  }
}
