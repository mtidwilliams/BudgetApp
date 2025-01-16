
  export function showSpinner(element: Element) {
    let spinner = document.createElement('span');
    spinner.classList.add('spinner-border', 'spinner-border-sm', 'ms-2', 'budgetSpinner');
    spinner.setAttribute('role', 'status');
    spinner.setAttribute('aria-hidden', 'true');

    // Append spinner to the link
    element.appendChild(spinner);
  }

  export function clearSpinner() {
    document.querySelectorAll('.budgetSpinner').forEach(el => el.remove());
  }