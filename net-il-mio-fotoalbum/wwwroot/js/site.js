// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



const loadFotos = filter => getFotos(filter)
    .then(fotos => {
        renderFotos(fotos);
        initButtons();
    });

const getFotos = title => axios
    .get('/api/FotoApi', title ? { params: { title } } : {})
    .then(res => res.data);

    
const fotoComponent = foto => `

            <div class="col ">
                <div class="card">
                    <img src="${foto.url}" class="card-img-top" alt="${foto.title}">
                    <div class="card-body d-flex justify-content-between">
                        <span class="">
                            <h5 class="card-name">${foto.title}</h5>
                            <p class="card-text">${foto.description}</p>
                        </span>
                        <a class="btn btn-primary btn-lg" href="Home/Show/${foto.id}">Dettagli</a>
                    </div>
                        
                </div>
            </div>`; 




const renderFotos = fotos => {
    const noFotos = document.querySelector("#no-fotos");
    const loader = document.querySelector("#fotos-loader");
    const fotosTbody = document.querySelector("#fotos");
    const fotosTable = document.querySelector("#fotos-table");
    const fotoFilter = document.querySelector("#fotos-filter");

    if (fotos && fotos.length > 0) {
        fotosTable.classList.add("show");
        fotoFilter.classList.add("show");
        noFotos.classList.add("hide");
    }
    else noFotos.classList.add("show");

    loader.classList.add("hide");

    fotosTbody.innerHTML = fotos.map(fotoComponent).join('');
};

const initFilter = () => {
    const filter = document.querySelector("#fotos-filter input");
    filter.addEventListener("input", (e) => loadFotos(e.target.value))
};



//______________________FORM__________________\\


const initCreateForm = () => {
    const form = document.querySelector("#mex-create-form");

    form.addEventListener("submit", e => {
        e.preventDefault();

        const mex = getMessaggioFromForm(form);

        mexPost(mex);
    });
};

function getMessaggioFromForm(form) {

    const email = form.querySelector("#Email").value;
    const mex = form.querySelector("#Messaggio").value;

    return {email:email , testoMessaggio:mex}
}


  const mexPost = mex => axios
        .post("/api/messaggiapi", mex)
        .then(() => location.href = "/")
        .catch(err => renderErrors(err.response.data.errors));
