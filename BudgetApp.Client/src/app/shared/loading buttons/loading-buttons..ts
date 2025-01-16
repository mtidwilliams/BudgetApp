import { showSpinner } from '../spinner-utils';

export class LoadingButtons {
    constructor() {
        const loadingButtonElements = document.querySelectorAll('.loading-button');

        loadingButtonElements.forEach(el => {
            let button = el as HTMLButtonElement;
            button.addEventListener('click', (event) => {
                let originalClickHandler = button.onclick;
                event.stopImmediatePropagation();
                showSpinner(button);

                if(button.type !== 'submit' && originalClickHandler) {
                    setTimeout(() => {
                        originalClickHandler.call(button, event);
                    }, 200);
                }
            });
        });
    }
}
