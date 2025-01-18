document.addEventListener("DOMContentLoaded", function () {
    const triggers = document.querySelectorAll("[data-modal-binding]");

    triggers.forEach(function (trigger) {
        trigger.addEventListener("click", function (e) {
            e.stopPropagation();
            const dialog = document.querySelector(`dialog[data-modal=${trigger.dataset.modalBinding}]`);
            if (dialog == null) {
                console.error(`No dialog found for trigger: ${trigger.dataset.modalBinding}`);
                return;
            }
            dialog.showModal(); 
            dialog.querySelector("[data-modal-close]").addEventListener("click", function () {
                dialog.close();
            });
        });
    });
});