// ===============================
// SITE JS - VION
// ===============================

// Executa quando o DOM estiver pronto
document.addEventListener("DOMContentLoaded", () => {
    ativarConfirmacoes();
    destacarMenuAtivo();
});

// ===============================
// CONFIRMAÇÕES (futuro admin)
// ===============================

function ativarConfirmacoes() {
    document.querySelectorAll("[data-confirm]").forEach(el => {
        el.addEventListener("click", e => {
            const msg = el.getAttribute("data-confirm");
            if (!confirm(msg)) {
                e.preventDefault();
            }
        });
    });
}

// ===============================
// MENU ATIVO AUTOMÁTICO
// ===============================

function destacarMenuAtivo() {
    const currentPath = window.location.pathname.toLowerCase();

    document.querySelectorAll(".nav-link").forEach(link => {
        const href = link.getAttribute("href")?.toLowerCase();
        if (href && currentPath === href) {
            link.classList.add("active");
            link.style.fontWeight = "600";
            link.style.color = "#9ecbff";
        }
    });
}
