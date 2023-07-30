import axios from 'axios'
import React, { useContext, useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom'
import { Button } from 'reactstrap'
import DefaultContext from '../contexts/DefaultContext'
import alertify from 'alertifyjs'

const Hospitals = () => {
    const {token} = useContext(DefaultContext)
    const [hospitals, setHospitals] = useState([])

    useEffect(() => {
        axios({
            method:"Get",
            headers:{
                Authorization: `Bearer ${token}`
            },
            baseURL:'https://localhost:7195/api',
            url:'/hospitals'
        })
            .then(response => setHospitals(response.data))
        console.log(hospitals)
    }, [])

    const deleteHospital = event => {
        let id = event.target.getAttribute('data-id')
        axios({
            method:"Delete",
            baseURL:'https://localhost:7195/api',
            url:`/hospitals/${id}`,
            headers:{
                "Authorization":`Bearer ${token}`
            },
        }).then(()=>{
            let child = event.target.parentNode.parentNode
            let parent = event.target.parentNode.parentNode.parentNode
            parent.removeChild(child);
            alertify.success('Talep silindi')
        })
        .catch(()=>{
            alertify.error('Talep silime işleminde bir hata oluştu')
        })
    }

    return (
        <div>
            <ol className="list-group list-group-numbered">
                {
                    hospitals.length > 0 && hospitals.map((hospital,index) => (
                        <li key={index} className="list-group-item d-flex justify-content-between">
                            <span>{hospital.name}</span>
                            <span className="btn-group">
                                <NavLink to={`/hospitals/employeeManagement/${hospital.id}`} className="btn btn-outline-dark">Personel Ekle/Çıkar</NavLink>
                                <NavLink to={`/hospitals/updateHospital/${hospital.id}`} className="btn btn-outline-dark">Düzenle</NavLink>
                                <button color='light' className="btn btn-outline-dark" data-id={hospital.id} onClick={deleteHospital}>Sil</button>
                            </span>
                        </li>
                    ))
                }
            </ol>


        </div>
    )
}

export default Hospitals