import axios from 'axios'
import React, { useContext, useEffect, useState } from 'react'
import { NavLink } from 'react-router-dom'
import { Button } from 'reactstrap'
import DefaultContext from '../contexts/DefaultContext'

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
    return (
        <div>
            <ol className="list-group list-group-numbered">
                {
                    hospitals.length > 0 && hospitals.map((hospital,index) => (
                        <li key={index} className="list-group-item d-flex justify-content-between">
                            <span>{hospital.name}</span>
                            <span className="btn-group">
                                <NavLink to={`/hospital/employeeManagement/${hospital.id}`} className="btn btn-outline-dark">Personel Ekle/Çıkar</NavLink>
                                <NavLink to={`/hospital/updateHospital/${hospital.id}`} className="btn btn-outline-dark">Düzenle</NavLink>
                                <Button color='light' className="btn btn-outline-dark">Sil</Button>
                            </span>
                        </li>
                    ))
                }
            </ol>


        </div>
    )
}

export default Hospitals