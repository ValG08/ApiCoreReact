import { config } from '../helpers/config';
import ResponseOrError from '../helpers/responseOrError';

export const noteService = {
    addNote,
    getAll,
    getById,
    edit,
    delete: _delete
};

function addNote(note) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(note)
    };
    
    return fetch(config.apiUrl + '/notes', requestOptions)
            .then(ResponseOrError.handleResponse, ResponseOrError.handleError);
};

function edit(id, note) {
    const requestOptions = {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(note)
    };
    
    return fetch(config.apiUrl + '/notes/' + id, requestOptions)
            .then(ResponseOrError.handleResponse, ResponseOrError.handleError);
};

function getAll() {   
    const requestOptions = {
        method: "GET",
        headers: { 'Content-Type': 'application/json' },    
    };
    
    return fetch(config.apiUrl + "/notes", requestOptions)
            .then(ResponseOrError.handleResponse, ResponseOrError.handleError);
}

function getById(id) {
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },    
    };

    return fetch(config.apiUrl + '/notes/' + id, requestOptions)
            .then(ResponseOrError.handleResponse, ResponseOrError.handleError);
}

function _delete(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    };

    return fetch(config.apiUrl + '/notes/' + id, requestOptions)
            .then(ResponseOrError.handleResponse, ResponseOrError.handleError);
}