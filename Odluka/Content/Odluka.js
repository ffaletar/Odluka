

function prikaziPopup() {
    document.getElementById('alternativa_popup').style.display = "block";
}

function prikaziPopupUrediAlternativu(id, nazivAlternative, opisAlternative) {
    document.getElementById('uredi_alternativu_popup').style.display = "block";
    document.getElementById('id_alternativa_uredi_alternativu').value = id;
    document.getElementById('naziv_alternative_input_uredi_alternativu').value = nazivAlternative;
    document.getElementById('opis_alternative_input_uredi_alternativu').value = opisAlternative;
}

function prikaziPopupNoviKriterij(idProjekta, idRoditelja) {
    var roditeljID;
    if (idRoditelja == null) {
        roditeljID = 0;
    } else {
        roditeljID = idRoditelja;
    }
    document.getElementById('kriterij_popup').style.display = "block";
    document.getElementById('id_roditelja').value = roditeljID;
    document.getElementById('id_projekt').value = idProjekta;
}

function promijeniBojuElementaListe() {
    document.getElementById('kriteriji_list_element_id').style.backgroundColor = red;
}