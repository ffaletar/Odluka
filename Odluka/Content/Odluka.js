

function prikaziHoverProjekt(idElementaProjekt) {
    document.getElementById(idElementaProjekt).style.display = "block";
}

function sakrijHoverProjekt(idElementaProjekt) {
    document.getElementById(idElementaProjekt).style.display = "none";
}


function prikaziPopupNovaAlternativa() {
    document.getElementById('nova_alternativa_popup').style.display = "block";
    document.getElementById('zatamnjivac_pozadine').style.display = "block";
}
function prikaziPopupUrediAlternativu(id, nazivAlternative, opisAlternative) {
    document.getElementById('uredi_alternativu_popup').style.display = "block";
    document.getElementById('zatamnjivac_pozadine').style.display = "block";
    document.getElementById('id_alternativa_uredi_alternativu').value = id;
    document.getElementById('popup_naziv_input_uredi').value = nazivAlternative;
    document.getElementById('popup_opis_input_uredi').value = opisAlternative;
}
function prikaziPopupNoviProjekt() {
    document.getElementById('projekt_popup').style.display = "block";
    document.getElementById('zatamnjivac_pozadine').style.display = "block";
}
function prikaziPopupNoviKriterij(idProjekta, idRoditelja, nazivRoditelja) {
    var roditeljID;
    if (idRoditelja == null) {
        roditeljID = 0;
    } else {
        roditeljID = idRoditelja;
    }
    document.getElementById('novi_kriterij_popup').style.display = "block";
    document.getElementById('zatamnjivac_pozadine').style.display = "block";
    if (nazivRoditelja) {
        document.getElementById('novi_kriterij_heading').innerHTML = "Novi podkriterij - " + nazivRoditelja;
    }
    
    document.getElementById('id_roditelja').value = roditeljID;
    document.getElementById('id_projekt').value = idProjekta;
}

function sakrijPopupove() {
    var zatamnjivacPostoji = document.getElementById("zatamnjivac_pozadine");
    var urediAlternativuPostoji = document.getElementById("uredi_alternativu_popup");
    var novaAlternativaPostoji = document.getElementById("nova_alternativa_popup");
    var projektPostoji = document.getElementById("projekt_popup");
    var noviKriterijPostoji = document.getElementById("novi_kriterij_popup");

    if (zatamnjivacPostoji) {
        document.getElementById('zatamnjivac_pozadine').style.display = "none";
    }
    if (urediAlternativuPostoji) {
        document.getElementById('uredi_alternativu_popup').style.display = "none";
    }
    if (novaAlternativaPostoji) {
        document.getElementById('nova_alternativa_popup').style.display = "none";
    }
    if (projektPostoji) {
        document.getElementById('projekt_popup').style.display = "none";
    }
    if (noviKriterijPostoji) {
        document.getElementById('novi_kriterij_popup').style.display = "none";
    }
    
}





function promijeniBojuElementaListe() {
    document.getElementById('kriteriji_list_element_id').style.backgroundColor = red;
}