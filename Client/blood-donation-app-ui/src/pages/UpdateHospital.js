import React, { useEffect } from 'react'
import { useParams } from 'react-router-dom';
import UpdateHospitalForm from '../components/UpdateHospitalForm';

const UpdateHospital = () => {
    const { id } = useParams();   
    return (
        <div>
            <UpdateHospitalForm id={id} />
        </div>
    )
}

export default UpdateHospital