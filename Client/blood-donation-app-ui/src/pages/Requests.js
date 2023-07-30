import axios from 'axios'
import React, { useContext, useEffect, useState } from 'react'
import DefaultContext from '../contexts/DefaultContext'
import { NavLink } from 'react-router-dom'
import { Button } from 'reactstrap'

const Requests = () => {
    const {token} = useContext(DefaultContext)
    const [requests, setRequests] = useState([])

    useEffect(() => {        
        axios({
            method:"Get",
            baseURL:'https://localhost:7195/api',
            url:"/requests/user",
            headers:{
                "Authorization":`Bearer ${token}`
            }
        })
            .then(response => setRequests(response.data))
        console.log(requests)
    }, [])
  return (
    <div>
   <ol className="list-group list-group-numbered">
                {
                    requests.length > 0 && requests.map((request,index) => (
                        <li  key={index} className="list-group-item d-flex justify-content-between align-items-start">
                        <div className="ms-2 me-auto">
                            <div className="fw-bold">{request.bloodGroup}</div>
                            <p>Miktar: <span className="badge bg-primary rounded-pill">{request.quantity}</span></p>
                        </div>
                        <div className="btn-group">
                        <NavLink to={`/hospital/employeeManagement/${request.id}`} className="btn btn-outline-dark">Düzenle</NavLink>
                            <Button type="button" color='light' className="btn btn-outline-dark">Sil</Button>
                        </div>
                    </li>
                    ))
                }
            </ol>
    </div>
  )
}

export default Requests