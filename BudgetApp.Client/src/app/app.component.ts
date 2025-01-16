import { Component, AfterViewChecked } from '@angular/core';
import { LoadingButtons } from './shared/loading buttons/loading-buttons.';
import { LoadingLinks } from './shared/loading buttons/loading-links';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements AfterViewChecked {
  title = 'app';
  ngAfterViewChecked(): void {
    new LoadingButtons();
    new LoadingLinks();
  }
}
