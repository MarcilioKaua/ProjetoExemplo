// Validações no formulário
async function validarFormulario() {
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

    // Verifica a existência do NPU no banco
    try {
        const npuExiste = await verificarNpuExistente(npuInput);
        if (npuExiste) {
            showToast("O NPU informado já existe.");
            return false;
        }
    } catch (error) {
        console.error("Erro ao verificar o NPU:", error);
        showToast("Erro ao verificar o NPU. Tente novamente.");
        return false;
    }

    // Verifica se todos os campos obrigatórios estão preenchidos
    const nome = document.getElementById("Name").value.trim();
    const uf = document.getElementById("Uf").value.trim();
    const municipioId = document.getElementById("MunicipioId").value.trim();

    if (!nome || !uf || !municipioId) {
        showToast("Por favor, preencha todos os campos obrigatórios.");
        return false;
    }

    return true;
}

// Envia o formulário via AJAX
document.getElementById("createForm").addEventListener("submit", async function (event) {
    event.preventDefault(); // Impede o envio tradicional do formulário

    const isValid = await validarFormulario();
    if (!isValid) return;

    const form = event.target;
    const formData = new FormData(form);

    try {
        const response = await fetch(form.action, {
            method: "POST",
            body: formData,
        });

        const result = await response.json();

        if (result.success) {
            if (result.redirectTo) {
                window.location.href = result.redirectTo; // Redireciona para a página de índice
            }
        } else {
            showToast(result.message); // Exibe a mensagem de erro no toast
        }
    } catch (error) {
        console.error("Erro ao enviar o formulário:", error);
        showToast("Erro ao enviar o formulário. Tente novamente.");
    }
});

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

// Carrega municípios com base na UF selecionada
async function carregarMunicipios() {
    const uf = document.getElementById("Uf").value;
    const municipioSelect = document.getElementById("MunicipioId");
    municipioSelect.innerHTML = '<option value="">Carregando...</option>';

    if (uf) {
        try {
            const response = await fetch(`/Processo/GetMunicipios?uf=${uf}`);
            const municipios = await response.json();

            municipioSelect.innerHTML = '<option value="">Selecione o município</option>';
            municipios.forEach((m) => {
                const option = document.createElement("option");
                option.value = m.id;
                option.text = m.nome;
                municipioSelect.appendChild(option);
            });

            municipioSelect.onchange = function () {
                const selectedOption = municipioSelect.options[municipioSelect.selectedIndex];
                document.getElementById("MunicipioNome").value = selectedOption.text;
            };
        } catch (error) {
            console.error("Erro ao carregar municípios:", error);
            showToast("Erro ao carregar municípios. Tente novamente.");
        }
    } else {
        municipioSelect.innerHTML = '<option value="">Selecione a UF primeiro</option>';
    }
}

// Verifica a existência do NPU
async function verificarNpuExistente(npu) {
    try {
        const response = await fetch(`/Processo/VerificarNpuExistente?npu=${encodeURIComponent(npu)}`);
        const data = await response.json();
        return data.existe;
    } catch (error) {
        console.error("Erro ao verificar o NPU:", error);
        throw error; // Relança o erro para tratamento
    }
}
