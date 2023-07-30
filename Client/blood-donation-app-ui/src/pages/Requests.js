import axios from 'axios'
import React, { useContext, useEffect, useState } from 'react'
import DefaultContext from '../contexts/DefaultContext'
import { NavLink } from 'react-router-dom'
import alertify from 'alertifyjs'

const Requests = () => {
    const { token } = useContext(DefaultContext)
    const [requests, setRequests] = useState([])

    useEffect(() => {
        axios({
            method: "Get",
            baseURL: 'https://localhost:7195/api',
            url: "/requests/user",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        })
            .then(response => setRequests(response.data))

    }, [])

    const deleteRequest = event => {
        let id = event.target.getAttribute('data-id')
        axios({
            method: "Delete",
            baseURL: 'https://localhost:7195/api',
            url: `/requests/${id}`,
            headers: {
                "Authorization": `Bearer ${token}`
            },
        }).then(() => {
            let child = event.target.parentNode.parentNode
            let parent = event.target.parentNode.parentNode.parentNode
            parent.removeChild(child);
            alertify.success('Talep silindi')
        })
            .catch(() => {
                alertify.error('Talep silime işleminde bir hata oluştu')
            })
    }

    return (
        <div>
            <ol className="list-group list-group-numbered">
                {
                    requests.length > 0 && requests.map((request, index) => (
                        <li key={index} className="list-group-item d-flex justify-content-between align-items-start">
                            <div className="ms-2 me-auto">
                                <div className="fw-bold">{request.bloodGroup}</div>
                                <p>Miktar: <span className="badge bg-primary rounded-pill">{request.quantity}</span></p>
                            </div>
                            <div className="btn-group">
                                <NavLink to={`/requests/updateRequest/${request.id}`} className="btn btn-outline-dark">Düzenle</NavLink>
                                <button type="button" className="btn btn-outline-dark" data-id={request.id} onClick={deleteRequest}>Sil</button>
                            </div>
                        </li>
                    ))
                }
            </ol>
        </div>
    )
}

export default Requests