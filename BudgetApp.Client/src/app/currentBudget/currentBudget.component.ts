import { Component, Input } from "@angular/core";
import { Budget } from "../entities/Budget";

@Component({
  selector: 'app-currentBudget',
  templateUrl: './currentBudget.component.html',
})
export class CurrentBudgetComponent {
  @Input() budget!: Budget;
}
