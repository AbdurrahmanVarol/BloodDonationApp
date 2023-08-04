import axios from 'axios'
import React, { useContext, useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom'
import { Button } from 'reactstrap'
import DefaultContext from '../contexts/DefaultContext'
import alertify from 'alertifyjs'

const Hospitals = () => {
    const { token } = useContext(DefaultContext)
    const [hospitals, setHospitals] = useState([])

    useEffect(() => {
        loadData()
    }, [])

    const loadData = () => {
        axios({
            method: "Get",
            headers: {
                Authorization: `Bearer ${token}`
            },
            baseURL: 'https://localhost:7195/api',
            url: '/hospitals'
        })
            .then(response => setHospitals(response.data))
    }

    const deleteHospital = hospitalId => {
       
        axios({
            method: "Delete",
            baseURL: 'https://localhost:7195/api',
            url: `/hospitals/${hospitalId}`,
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
                    hospitals.length > 0 && hospitals.map((hospital, index) => (
                        <li key={index} className="list-group-item d-flex justify-content-between">
                            <span>{hospital.name}</span>
                            <span className="btn-group">
                                <NavLink to={`/hospitals/employeeManagement/${hospital.id}`} className="btn btn-outline-dark">Personel Ekle/Çıkar</NavLink>
                                <NavLink to={`/hospitals/updateHospital/${hospital.id}`} className="btn btn-outline-dark">Düzenle</NavLink>
                                <button color='light' className="btn btn-outline-dark" onClick={() => deleteHospital(hospital.id)}>Sil</button>
                            </span>
                        </li>
                    ))
                }
            </ol>
        </div>
    )
}

export default Hospitals