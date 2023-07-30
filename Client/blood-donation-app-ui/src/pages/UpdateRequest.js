import React from 'react'
import { useParams } from 'react-router-dom';
import UpdateRequestForm from '../components/UpdateRequestForm';

const UpdateRequest = () => {
    const { id } = useParams();
    return (
        <div>
            <UpdateRequestForm id={id} />
        </div>
    )
}

export default UpdateRequest