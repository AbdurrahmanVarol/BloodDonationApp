import axios from 'axios'
import React, { useEffect, useState } from 'react'

const Cities = ({ handleChange, value }) => {
  const [cities, setCities] = useState([])

  useEffect(() => {
    axios('https://localhost:7195/api/Cities')
      .then(response => setCities(response.data))
  }, [])
  return (
    <div className='form-floating'>
      <select id='cityId' name='cityId' className='form-select' onChange={handleChange} value={value}>
        <option value={0} hidden>Seçiniz</option>
        {
          cities.length > 0 && cities.map(city => (
            <option key={city.id} value={city.id}>{`${city.name}(${city.plate})`}</option>
          ))
        }
      </select>
      <label htmlFor="cityId">Şehir</label>
    </div>
  )
}

export default Cities