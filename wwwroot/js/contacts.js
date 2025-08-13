// Variables globales
let currentPage = 1;
let pageSize = 12;
let currentSearch = '';
let currentSort = 'lastName_asc';
let contacts = [];
let isEditMode = false;
let editingContactId = null;

// Initialisation de la page
$(document).ready(function() {
    initializeEventHandlers();
    loadContacts();
    
    // Check if we need to open edit modal from URL
    const urlParams = new URLSearchParams(window.location.search);
    const editId = urlParams.get('edit');
    if (editId) {
        setTimeout(() => openContactModal(parseInt(editId)), 500);
    }
});

// Gestionnaires d'événements
function initializeEventHandlers() {
    // Search input
    $('#searchInput, #globalSearch').on('input', debounce(function() {
        currentSearch = $(this).val().trim();
        currentPage = 1;
        loadContacts();
    }, 300));

    // Sort select
    $('#sortSelect').on('change', function() {
        currentSort = $(this).val();
        currentPage = 1;
        loadContacts();
    });

    // Contact form submission
    $('#contactForm').on('submit', handleContactSubmit);

    // Modal reset on close
    $('#contactModal').on('hidden.bs.modal', resetContactModal);
}

// Charger les contacts
async function loadContacts() {
    try {
        showLoading();
        
        const [sortBy, sortOrder] = currentSort.split('_');
        const params = new URLSearchParams({
            pageNumber: currentPage,
            pageSize: pageSize,
            sortBy: sortBy,
            sortOrder: sortOrder
        });
        
        if (currentSearch) {
            params.append('searchTerm', currentSearch);
        }

        const response = await fetch(`/api/contacts?${params}`);
        
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        contacts = data.items;
        
        renderContacts(data.items);
        renderPagination(data);
        updateStats(data.items);
        
        // Show/hide empty state
        if (data.items.length === 0 && currentSearch === '') {
            document.getElementById('emptyState').style.display = 'block';
            document.getElementById('contactsGrid').style.display = 'none';
        } else {
            document.getElementById('emptyState').style.display = 'none';
            document.getElementById('contactsGrid').style.display = 'block';
        }
        
    } catch (error) {
        console.error('Error loading contacts:', error);
        showToast('Erreur lors du chargement des contacts', 'danger');
    } finally {
        hideLoading();
    }
}

// Afficher les contacts
function renderContacts(contactsData) {
    const grid = document.getElementById('contactsGrid');
    
    // Stocker les contacts actuels pour le redimensionnement
    currentContacts = contactsData || [];
    
    if (contactsData.length === 0 && currentSearch !== '') {
        grid.innerHTML = `
            <div class="col-12 text-center py-5">
                <i class="fas fa-search fa-3x text-muted mb-3"></i>
                <h4>Aucun résultat trouvé</h4>
                <p class="text-muted">Essayez avec d'autres termes de recherche</p>
                <button class="btn btn-outline-primary" onclick="clearSearch()">
                    Effacer la recherche
                </button>
            </div>
        `;
        return;
    }
    
    // Déterminer si on est sur desktop ou mobile
    const isDesktop = window.innerWidth >= 768;
    
    const html = contactsData.map(contact => {
        const initials = (contact.firstName.charAt(0) + contact.lastName.charAt(0)).toUpperCase();
        
        if (isDesktop) {
            // Layout horizontal pour desktop
            return `
                <div class="col-12 mb-3 fade-in">
                    <div class="modern-card contact-card contact-card-horizontal">
                        <div class="contact-avatar">
                            ${initials}
                        </div>
                        <div class="contact-info">
                            <div class="contact-name">${contact.fullName}</div>
                            <div class="contact-email">${contact.email}</div>
                        </div>
                        <div class="contact-details">
                            ${contact.phoneNumber ? `
                            <div>
                                <i class="fas fa-phone text-success me-2"></i>
                                <a href="tel:${contact.phoneNumber}" class="text-decoration-none">${contact.phoneNumber}</a>
                            </div>
                            ` : '<div></div>'}
                            ${contact.address ? `
                            <div>
                                <i class="fas fa-map-marker-alt text-info me-2"></i>
                                <span class="text-muted">${contact.address.length > 50 ? contact.address.substring(0, 50) + '...' : contact.address}</span>
                            </div>
                            ` : '<div></div>'}
                            <div>
                                <i class="fas fa-calendar-plus text-muted me-2"></i>
                                <span class="text-muted small">${new Date(contact.createdAt).toLocaleDateString('fr-FR')}</span>
                            </div>
                        </div>
                        <div class="contact-actions">
                            <button class="btn btn-icon btn-outline-primary me-2" onclick="viewContact(${contact.id})" title="Voir les détails">
                                <i class="fas fa-eye"></i>
                            </button>
                            <button class="btn btn-icon btn-outline-secondary me-2" onclick="openContactModal(${contact.id})" title="Modifier">
                                <i class="fas fa-edit"></i>
                            </button>
                            <button class="btn btn-icon btn-outline-danger" onclick="deleteContact(${contact.id})" title="Supprimer">
                                <i class="fas fa-trash"></i>
                            </button>
                        </div>
                    </div>
                </div>
            `;
        } else {
            // Layout vertical pour mobile
            return `
                <div class="col-md-6 mb-4 fade-in">
                    <div class="modern-card contact-card h-100">
                        <div class="card-body p-4">
                            <div class="d-flex align-items-center mb-4">
                                <div class="contact-avatar me-3">
                                    ${initials}
                                </div>
                                <div class="contact-info">
                                    <div class="contact-name">${contact.fullName}</div>
                                    <div class="contact-email">${contact.email}</div>
                                </div>
                            </div>
                            
                            <div class="contact-details mb-4">
                                ${contact.phoneNumber ? `
                                <div class="d-flex align-items-center mb-2">
                                    <i class="fas fa-phone text-success me-3"></i>
                                    <a href="tel:${contact.phoneNumber}" class="text-decoration-none">${contact.phoneNumber}</a>
                                </div>
                                ` : ''}
                                ${contact.address ? `
                                <div class="d-flex align-items-center mb-2">
                                    <i class="fas fa-map-marker-alt text-info me-3"></i>
                                    <span class="text-muted small">${contact.address.length > 40 ? contact.address.substring(0, 40) + '...' : contact.address}</span>
                                </div>
                                ` : ''}
                                <div class="d-flex align-items-center text-muted small">
                                    <i class="fas fa-calendar-plus me-3"></i>
                                    ${new Date(contact.createdAt).toLocaleDateString('fr-FR')}
                                </div>
                            </div>
                            
                            <div class="contact-actions">
                                <button class="btn btn-icon btn-outline-primary" onclick="viewContact(${contact.id})" title="Voir les détails">
                                    <i class="fas fa-eye"></i>
                                </button>
                                <button class="btn btn-icon btn-outline-secondary" onclick="openContactModal(${contact.id})" title="Modifier">
                                    <i class="fas fa-edit"></i>
                                </button>
                                <button class="btn btn-icon btn-outline-danger" onclick="deleteContact(${contact.id})" title="Supprimer">
                                    <i class="fas fa-trash"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            `;
        }
    }).join('');
    
    grid.innerHTML = html;
}

// Afficher la pagination
function renderPagination(data) {
    const pagination = document.getElementById('pagination');
    
    if (data.totalPages <= 1) {
        pagination.innerHTML = '';
        return;
    }
    
    let html = '';
    
    // Previous button
    html += `
        <li class="page-item ${!data.hasPreviousPage ? 'disabled' : ''}">
            <a class="page-link" href="#" onclick="changePage(${data.pageNumber - 1})">
                <i class="fas fa-chevron-left"></i>
            </a>
        </li>
    `;
    
    // Page numbers
    const startPage = Math.max(1, data.pageNumber - 2);
    const endPage = Math.min(data.totalPages, data.pageNumber + 2);
    
    if (startPage > 1) {
        html += `<li class="page-item"><a class="page-link" href="#" onclick="changePage(1)">1</a></li>`;
        if (startPage > 2) {
            html += `<li class="page-item disabled"><span class="page-link">...</span></li>`;
        }
    }
    
    for (let i = startPage; i <= endPage; i++) {
        html += `
            <li class="page-item ${i === data.pageNumber ? 'active' : ''}">
                <a class="page-link" href="#" onclick="changePage(${i})">${i}</a>
            </li>
        `;
    }
    
    if (endPage < data.totalPages) {
        if (endPage < data.totalPages - 1) {
            html += `<li class="page-item disabled"><span class="page-link">...</span></li>`;
        }
        html += `<li class="page-item"><a class="page-link" href="#" onclick="changePage(${data.totalPages})">${data.totalPages}</a></li>`;
    }
    
    // Next button
    html += `
        <li class="page-item ${!data.hasNextPage ? 'disabled' : ''}">
            <a class="page-link" href="#" onclick="changePage(${data.pageNumber + 1})">
                <i class="fas fa-chevron-right"></i>
            </a>
        </li>
    `;
    
    pagination.innerHTML = html;
}

// Mettre à jour les statistiques
function updateStats(contactsData) {
    // Note: Ces stats sont basées sur la page courante, pas sur tous les contacts
    const totalContacts = contactsData.length;
    const contactsWithPhone = contactsData.filter(c => c.phoneNumber).length;
    const contactsWithAddress = contactsData.filter(c => c.address).length;
    
    document.getElementById('totalContacts').textContent = totalContacts;
    document.getElementById('contactsWithPhone').textContent = contactsWithPhone;
    document.getElementById('contactsWithAddress').textContent = contactsWithAddress;
}

// Changer de page
function changePage(page) {
    if (page < 1) return;
    currentPage = page;
    loadContacts();
    
    // Scroll to top
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

// Ouvrir le modal de contact
async function openContactModal(contactId = null) {
    isEditMode = !!contactId;
    editingContactId = contactId;
    
    const modal = document.getElementById('contactModal');
    const modalTitle = document.getElementById('contactModalLabel');
    const form = document.getElementById('contactForm');
    
    // Reset form
    form.reset();
    form.classList.remove('was-validated');
    clearValidationErrors();
    
    if (isEditMode) {
        modalTitle.innerHTML = '<i class="fas fa-edit me-2"></i>Modifier le Contact';
        
        try {
            showLoading();
            const response = await fetch(`/api/contacts/${contactId}`);
            
            if (!response.ok) {
                throw new Error('Contact non trouvé');
            }
            
            const contact = await response.json();
            populateForm(contact);
            
        } catch (error) {
            console.error('Error loading contact for edit:', error);
            showToast('Erreur lors du chargement du contact', 'danger');
            return;
        } finally {
            hideLoading();
        }
    } else {
        modalTitle.innerHTML = '<i class="fas fa-user-plus me-2"></i>Nouveau Contact';
    }
    
    const bootstrapModal = new bootstrap.Modal(modal);
    bootstrapModal.show();
}

// Remplir le formulaire avec les données du contact
function populateForm(contact) {
    document.getElementById('firstName').value = contact.firstName || '';
    document.getElementById('lastName').value = contact.lastName || '';
    document.getElementById('email').value = contact.email || '';
    document.getElementById('phoneNumber').value = contact.phoneNumber || '';
    document.getElementById('address').value = contact.address || '';
}

// Gérer la soumission du formulaire
async function handleContactSubmit(e) {
    e.preventDefault();
    
    const form = e.target;
    
    // Validation côté client
    if (!form.checkValidity()) {
        form.classList.add('was-validated');
        return;
    }
    
    const contactData = {
        firstName: document.getElementById('firstName').value,
        lastName: document.getElementById('lastName').value,
        email: document.getElementById('email').value,
        phoneNumber: document.getElementById('phoneNumber').value,
        address: document.getElementById('address').value
    };
    
    try {
        showLoading();
        
        const url = isEditMode ? `/api/contacts/${editingContactId}` : '/api/contacts';
        const method = isEditMode ? 'PUT' : 'POST';
        
        const response = await fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(contactData)
        });
        
        if (!response.ok) {
            const errorData = await response.json().catch(() => ({ message: 'Erreur inconnue' }));
            throw new Error(errorData.message || `HTTP error! status: ${response.status}`);
        }
        
        const contact = await response.json();
        
        showToast(
            isEditMode ? '✅ Contact modifié avec succès' : '🎉 Contact créé avec succès', 
            'success'
        );
        
        // Close modal properly
        const modalElement = document.getElementById('contactModal');
        const modal = bootstrap.Modal.getInstance(modalElement);
        if (modal) {
            modal.hide();
        }
        
        // Force remove backdrop if it exists
        setTimeout(() => {
            const backdrop = document.querySelector('.modal-backdrop');
            if (backdrop) {
                backdrop.remove();
            }
            document.body.classList.remove('modal-open');
            document.body.style.overflow = '';
            document.body.style.paddingRight = '';
        }, 300);
        
        // Reset form and variables
        resetContactModal();
        
        // Reload contacts
        await loadContacts();
        
    } catch (error) {
        console.error('Error saving contact:', error);
        
        if (error.message.includes('existe déjà')) {
            showFieldError('email', 'Cette adresse email est déjà utilisée');
        } else {
            showToast(`❌ Erreur lors de ${isEditMode ? 'la modification' : 'la création'} du contact: ${error.message}`, 'danger');
        }
    } finally {
        hideLoading();
    }
}

// Supprimer un contact
async function deleteContact(contactId) {
    try {
        // Load contact details first
        const response = await fetch(`/api/contacts/${contactId}`);
        if (!response.ok) {
            throw new Error('Contact non trouvé');
        }
        
        const contact = await response.json();
        showDeleteConfirmation(contactId, contact.fullName, contact.email);
        
    } catch (error) {
        console.error('Error loading contact for deletion:', error);
        showToast('❌ Erreur lors du chargement du contact', 'danger');
    }
}

// Afficher le modal de confirmation de suppression
function showDeleteConfirmation(contactId, fullName, email) {
    const modal = new bootstrap.Modal(document.getElementById('deleteConfirmModal'));
    const preview = document.getElementById('deleteContactPreview');
    const confirmBtn = document.getElementById('confirmDeleteBtn');
    
    // Set contact preview
    const initials = fullName.split(' ').map(n => n.charAt(0)).join('').toUpperCase();
    preview.innerHTML = `
        <div class="d-flex align-items-center">
            <div class="contact-avatar me-3" style="width: 50px; height: 50px; font-size: 16px;">
                ${initials}
            </div>
            <div>
                <h6 class="mb-1">${fullName}</h6>
                <small class="text-muted">${email}</small>
            </div>
        </div>
    `;
    
    // Set up confirm button
    confirmBtn.onclick = () => confirmDeleteContact(contactId);
    
    modal.show();
}

// Confirmer la suppression
async function confirmDeleteContact(contactId) {
    const confirmBtn = document.getElementById('confirmDeleteBtn');
    const spinner = confirmBtn.querySelector('.loading-spinner');
    
    try {
        // Show loading
        spinner.style.display = 'inline-block';
        confirmBtn.disabled = true;
        
        const response = await fetch(`/api/contacts/${contactId}`, {
            method: 'DELETE'
        });
        
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        // Close modal
        const modal = bootstrap.Modal.getInstance(document.getElementById('deleteConfirmModal'));
        modal.hide();
        
        showToast('✅ Contact supprimé avec succès', 'success');
        await loadContacts();
        
        // Close details modal if open
        const detailsModal = bootstrap.Modal.getInstance(document.getElementById('contactDetailsModal'));
        if (detailsModal) detailsModal.hide();
        
    } catch (error) {
        console.error('Error deleting contact:', error);
        showToast('❌ Erreur lors de la suppression du contact', 'danger');
    } finally {
        spinner.style.display = 'none';
        confirmBtn.disabled = false;
    }
}

// Voir les détails d'un contact
async function viewContact(contactId) {
    try {
        // Show modal
        const modal = new bootstrap.Modal(document.getElementById('contactDetailsModal'));
        modal.show();
        
        // Load contact details
        const response = await fetch(`/api/contacts/${contactId}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        const contact = await response.json();
        renderContactDetailsModal(contact);
        
    } catch (error) {
        console.error('Error loading contact details:', error);
        showToast('❌ Erreur lors du chargement des détails', 'danger');
        
        // Close modal on error
        const modal = bootstrap.Modal.getInstance(document.getElementById('contactDetailsModal'));
        if (modal) modal.hide();
    }
}

// Render contact details in modal
function renderContactDetailsModal(contact) {
    const initials = (contact.firstName.charAt(0) + contact.lastName.charAt(0)).toUpperCase();
    const content = document.getElementById('contactDetailsContent');
    
    const html = `
        <div class="row g-4">
            <!-- Contact Header -->
            <div class="col-12">
                <div class="text-center mb-4">
                    <div class="contact-avatar mx-auto mb-3" style="width: 80px; height: 80px; font-size: 28px;">
                        ${initials}
                    </div>
                    <h3 class="mb-2">${contact.fullName}</h3>
                    <p class="text-muted">Contact depuis le ${new Date(contact.createdAt).toLocaleDateString('fr-FR')}</p>
                </div>
            </div>
            
            <!-- Contact Information -->
            <div class="col-md-6">
                <div class="modern-card h-100 p-4">
                    <h5 class="mb-4">
                        <i class="fas fa-info-circle me-2 text-primary"></i>
                        Informations de Contact
                    </h5>
                    
                    <div class="contact-detail-item mb-3">
                        <div class="d-flex align-items-center">
                            <i class="fas fa-envelope text-primary me-3"></i>
                            <div>
                                <small class="text-muted d-block">Email</small>
                                <a href="mailto:${contact.email}" class="text-decoration-none fw-medium">${contact.email}</a>
                            </div>
                        </div>
                    </div>
                    
                    ${contact.phoneNumber ? `
                    <div class="contact-detail-item mb-3">
                        <div class="d-flex align-items-center">
                            <i class="fas fa-phone text-success me-3"></i>
                            <div>
                                <small class="text-muted d-block">Téléphone</small>
                                <a href="tel:${contact.phoneNumber}" class="text-decoration-none fw-medium">${contact.phoneNumber}</a>
                            </div>
                        </div>
                    </div>
                    ` : ''}
                    
                    ${contact.address ? `
                    <div class="contact-detail-item mb-3">
                        <div class="d-flex align-items-center">
                            <i class="fas fa-map-marker-alt text-info me-3"></i>
                            <div>
                                <small class="text-muted d-block">Adresse</small>
                                <span class="fw-medium">${contact.address}</span>
                            </div>
                        </div>
                    </div>
                    ` : ''}
                </div>
            </div>
            
            <!-- Actions & Metadata -->
            <div class="col-md-6">
                <div class="modern-card h-100 p-4">
                    <h5 class="mb-4">
                        <i class="fas fa-cog me-2 text-secondary"></i>
                        Actions & Informations
                    </h5>
                    
                    <!-- Quick Actions -->
                    <div class="mb-4">
                        <div class="row g-2">
                            <div class="col-6">
                                <button class="btn btn-outline-primary w-100" onclick="sendEmail('${contact.email}')">
                                    <i class="fas fa-envelope me-1"></i>
                                    Email
                                </button>
                            </div>
                            ${contact.phoneNumber ? `
                            <div class="col-6">
                                <button class="btn btn-outline-success w-100" onclick="makeCall('${contact.phoneNumber}')">
                                    <i class="fas fa-phone me-1"></i>
                                    Appeler
                                </button>
                            </div>
                            ` : ''}
                        </div>
                    </div>
                    
                    <!-- Metadata -->
                    <div class="contact-detail-item mb-3">
                        <div class="d-flex align-items-center">
                            <i class="fas fa-calendar-plus text-warning me-3"></i>
                            <div>
                                <small class="text-muted d-block">Créé le</small>
                                <span class="fw-medium">${new Date(contact.createdAt).toLocaleString('fr-FR')}</span>
                            </div>
                        </div>
                    </div>
                    
                    <div class="contact-detail-item mb-3">
                        <div class="d-flex align-items-center">
                            <i class="fas fa-edit text-secondary me-3"></i>
                            <div>
                                <small class="text-muted d-block">Modifié le</small>
                                <span class="fw-medium">${new Date(contact.updatedAt).toLocaleString('fr-FR')}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Action Buttons -->
            <div class="col-12">
                <div class="d-flex justify-content-center gap-3">
                    <button class="btn btn-modern btn-modern-primary" onclick="editContactFromDetails(${contact.id})">
                        <i class="fas fa-edit me-2"></i>
                        Modifier le Contact
                    </button>
                    <button class="btn btn-outline-danger" onclick="showDeleteConfirmation(${contact.id}, '${contact.fullName}', '${contact.email}')">
                        <i class="fas fa-trash me-2"></i>
                        Supprimer
                    </button>
                </div>
            </div>
        </div>
    `;
    
    content.innerHTML = html;
}

// Modifier un contact depuis le modal de détails
function editContactFromDetails(contactId) {
    // Close details modal
    const detailsModal = bootstrap.Modal.getInstance(document.getElementById('contactDetailsModal'));
    if (detailsModal) detailsModal.hide();
    
    // Open edit modal
    setTimeout(() => {
        openContactModal(contactId);
    }, 300);
}

// Fonctions utilitaires pour les actions rapides
function sendEmail(email) {
    window.open(`mailto:${email}`, '_blank');
}

function makeCall(phone) {
    window.open(`tel:${phone}`, '_blank');
}

// Réinitialiser le modal
function resetContactModal() {
    const form = document.getElementById('contactForm');
    form.reset();
    form.classList.remove('was-validated');
    clearValidationErrors();
    
    // Reset modal title
    document.getElementById('contactModalLabel').innerHTML = '<i class="fas fa-user-plus me-3"></i>Nouveau Contact';
    
    // Reset variables
    isEditMode = false;
    editingContactId = null;
    
    // Ensure modal backdrop is completely removed
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop) {
        backdrop.remove();
    }
    document.body.classList.remove('modal-open');
    document.body.style.overflow = '';
    document.body.style.paddingRight = '';
}

// Effacer la recherche
function clearSearch() {
    document.getElementById('searchInput').value = '';
    document.getElementById('globalSearch').value = '';
    currentSearch = '';
    currentPage = 1;
    loadContacts();
}

// Exporter les contacts
function exportContacts() {
    showToast('Fonctionnalité d\'export en cours de développement', 'info');
}

// Utilitaires
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

function showFieldError(fieldId, message) {
    const field = document.getElementById(fieldId);
    const feedback = field.nextElementSibling;
    
    field.classList.add('is-invalid');
    if (feedback && feedback.classList.contains('invalid-feedback')) {
        feedback.textContent = message;
    }
}

function clearValidationErrors() {
    const form = document.getElementById('contactForm');
    const invalidFields = form.querySelectorAll('.is-invalid');
    
    invalidFields.forEach(field => {
        field.classList.remove('is-invalid');
    });
}

// Placeholder API pour les images
if (window.location.pathname.includes('/api/placeholder')) {
    // Generate a simple colored rectangle as placeholder
    const canvas = document.createElement('canvas');
    const ctx = canvas.getContext('2d');
    canvas.width = 200;
    canvas.height = 200;
    
    // Random color
    const colors = ['#007bff', '#28a745', '#dc3545', '#ffc107', '#17a2b8', '#6f42c1'];
    const color = colors[Math.floor(Math.random() * colors.length)];
    
    ctx.fillStyle = color;
    ctx.fillRect(0, 0, canvas.width, canvas.height);
    
    ctx.fillStyle = 'white';
    ctx.font = '24px Arial';
    ctx.textAlign = 'center';
    ctx.fillText('Contact', canvas.width/2, canvas.height/2);
    
    // This is just for demonstration - in a real app you'd serve actual placeholder images
}

// Variable globale pour stocker les contacts actuels
let currentContacts = [];

// Gestionnaire pour le redimensionnement de la fenêtre
window.addEventListener('resize', debounce(() => {
    // Recharger les contacts pour adapter le layout
    if (currentContacts.length > 0) {
        renderContacts(currentContacts);
    }
}, 250));