import axios from 'axios'
import React, { useContext, useEffect, useState } from 'react'
import DefaultContext from '../contexts/DefaultContext'
import { NavLink } from 'react-router-dom'
import alertify from 'alertifyjs'

const Requests = () => {
    const { token } = useContext(DefaultContext)
    const [requests, setRequests] = useState([])

    useEffect(() => {
        loadData()
    }, [])

    const loadData = () => {
        axios({
            method: "Get",
            baseURL: 'https://localhost:7195/api',
            url: "/requests/user",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        })
            .then(response => setRequests(response.data))
    }

    const deleteRequest = requestId => {
        axios({
            method: "Delete",
            baseURL: 'https://localhost:7195/api',
            url: `/requests/${requestId}`,
            headers: {
                "Authorization": `Bearer ${token}`
            },
        }).then(() => {
            alertify.success('Talep silindi')
            loadData()
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
                                <button type="button" className="btn btn-outline-dark" onClick={() => deleteRequest(request.id)}>Sil</button>
                            </div>
                        </li>
                    ))
                }
            </ol>
        </div>
    )
}

export default Requests