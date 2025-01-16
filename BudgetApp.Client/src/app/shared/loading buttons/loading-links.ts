import { showSpinner } from '../spinner-utils';

export class LoadingLinks {
  constructor() {
    // Select all anchor tags with class 'loading-link'
    const loadingLinkElements = document.querySelectorAll('.loading-link');
  
    // Attach event listeners to each link
    loadingLinkElements.forEach(el => {
      el.addEventListener('click', (event) => {
        event.preventDefault();
        event.stopImmediatePropagation();
        
        // Show loading spinner
        showSpinner(el);
  
        setTimeout(() => {
          window.location.href = (event.target as HTMLAnchorElement).href;
        }, 200);
      });
    });
  }
}
