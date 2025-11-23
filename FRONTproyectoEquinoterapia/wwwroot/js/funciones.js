window.getSelectedValues = (selectElement) => {
    return Array.from(selectElement.selectedOptions).map(o => o.value);
};
