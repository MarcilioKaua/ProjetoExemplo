const deleteModal = document.getElementById('deleteModal');
deleteModal.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget;
    const processId = button.getAttribute('data-id');
    const processName = button.getAttribute('data-name');

    const modalProcessName = deleteModal.querySelector('#processName');
    modalProcessName.textContent = processName;

    const deleteForm = deleteModal.querySelector('#deleteForm');
    deleteForm.action = `/Processo/Delete/${processId}`;
});