// Validações no formulário
function validarFormulario() {
    const npuInput = document.getElementById("Npu").value;
    const npuSemMascara = npuInput.replace(/\D/g, "");

    // Verifica se o comprimento total do NPU, incluindo formatação, é igual a 25
    if (npuInput.length !== 25) {
        showToast("O NPU deve conter exatamente 25 caracteres.");
        return false;
    }

    // Verifica o formato com a máscara
    const formatoValido = /^\d{7}-\d{2}\.\d{4}\.\d{1}\.\d{2}\.\d{4}$/.test(npuInput);
    if (!formatoValido) {
        showToast("O NPU não está no formato correto.");
        return false;
    }

    // Verifica se todos os campos obrigatórios estão preenchidos
    const nome = document.getElementById("Name").value.trim();
    const uf = document.getElementById("Uf").value.trim();

    if (!nome || !uf) {
        showToast("Por favor, preencha todos os campos obrigatórios.");
        return false;
    }

    return true; // Permite o envio se todas as validações forem bem-sucedidas
}

// Função para exibir o toast
function showToast(message) {
    const container = document.getElementById("toast-container");
    // Estrutura do Toast
    const toast = document.createElement("div");
    toast.className = `toast align-items-center bg-danger border-0`;
    toast.setAttribute("role", "alert");
    toast.setAttribute("aria-live", "assertive");
    toast.setAttribute("aria-atomic", "true");
    toast.innerHTML = `
                <div class="d-flex">
                    <div class="toast-body">${message}</div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
            `;

    // Adiciona o toast no container
    container.appendChild(toast);

    // Inicializa e exibe o toast com o Bootstrap
    const bootstrapToast = new bootstrap.Toast(toast);
    bootstrapToast.show();

    // Remove o toast automaticamente após sua exibição
    toast.addEventListener("hidden.bs.toast", () => {
        toast.remove();
    });
}