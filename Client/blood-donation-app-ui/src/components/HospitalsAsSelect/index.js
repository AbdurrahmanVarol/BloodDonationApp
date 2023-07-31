import axios from 'axios'
import React, { useContext, useEffect, useState } from 'react'
import DefaultContext from '../../contexts/DefaultContext'

const HospitalsAsSelect = ({ handleChange, value }) => {
    const { token } = useContext(DefaultContext)
    const [hospitals, setHospitals] = useState([])

    useEffect(() => {
        axios({
            method: "Get",
            baseURL: 'https://localhost:7195/api',
            url: "/hospitals",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        })
            .then(response => setHospitals(response.data))
        console.log(hospitals)
    }, [])
    return (
        <div className='form-floating'>
            <select id='hospitalId' name='hospitalId' className='form-select' onChange={handleChange} value={value}>
                <option value={0} hidden>Seçiniz</option>
                {
                    hospitals.length > 0 && hospitals.map(hospital => (
                        <option key={hospital.id} value={hospital.id}>{`${hospital.name} - ${hospital.city}`}</option>
                    ))
                }
            </select>
            <label htmlFor="cityId">Hastane</label>
        </div>
    )
}

export default HospitalsAsSelect