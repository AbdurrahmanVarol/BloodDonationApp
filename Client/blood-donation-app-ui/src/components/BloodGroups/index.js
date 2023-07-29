import axios from 'axios'
import React, { useEffect, useState } from 'react'

const BloodGroups = ({handleChange,value}) => {
    const [bloodGroups,setBloodGroups] = useState([])

    useEffect(()=>{
        axios( 'https://localhost:7195/api/BloodGroups')
        .then(response=>setBloodGroups(response.data))
        console.log(bloodGroups)
    },[])
  return (
    <div className='form-floating'>
        <select id='bloodGroupId' name='bloodGroupId' className='form-select' onChange={handleChange} value={value}>
            <option value={0} hidden>Seçiniz</option>
            {
                bloodGroups.length>0 && bloodGroups.map(bloodGroup=>(
                    <option key={bloodGroup.id} value={bloodGroup.id}>{bloodGroup.symbol}</option>
                ))
            }
        </select>
        <label htmlFor="BloodGroupId">Kan Grubu</label>
    </div>
  )
}

export default BloodGroups