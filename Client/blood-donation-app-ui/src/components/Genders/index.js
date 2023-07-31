import axios from 'axios'
import React, { useEffect, useState } from 'react'

const Genders = ({ handleChange, value }) => {
    const [genders, setGenders] = useState([])

    useEffect(() => {
        axios('https://localhost:7195/api/genders')
            .then(response => setGenders(response.data))
        console.log(genders)
    }, [])
    return (
        <div className='form-floating'>
            <select id='genderId' name='genderId' className='form-select' onChange={handleChange} value={value}>
                <option value={0} hidden>Seçiniz</option>
                {
                    genders.length > 0 && genders.map(gender => (
                        <option key={gender.id} value={gender.id}>{`${gender.name}`}</option>
                    ))
                }
            </select>
            <label htmlFor="cityId">Cinsiyet</label>
        </div>
    )
}

export default Genders