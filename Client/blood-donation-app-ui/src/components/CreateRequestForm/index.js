import { useFormik } from 'formik'
import React, { useContext, useEffect } from 'react'
import { Button, Form, FormGroup, Input } from 'reactstrap'
import Cities from '../Cities'
import alertify from 'alertifyjs'
import axios from 'axios'
import DefaultContext from '../../contexts/DefaultContext'
import HospitalsAsSelect from '../HospitalsAsSelect'
import BloodGroups from '../BloodGroups'
const CreateRequestForm = () => {
    const { token,userRole } = useContext(DefaultContext)

    const { handleSubmit, handleChange, handleBlur, setFieldValue, values, errors, touched, isSubmitting } = useFormik({
        initialValues: {
            bloodGroupId: "",
            hospitalId: null,
            quantity: ""
        },
        onSubmit: (values, bag) => {
            console.log(values)
            axios({
                baseURL: 'https://localhost:7195/api',
                url: '/requests',
                method: 'Post',
                headers: {
                    "Authorization": `Bearer ${token}`
                },
                data: values
            })
                .then(response => {
                    alertify.success('Talep oluşturuldu.')
                    bag.resetForm()
                })
                .catch(errors => {
                    alertify.error('Talep oluşturulurken bir hata oluştu.')
                })
        },
        //validationSchema
    })

    const increase = () => {
        let quantity = values.quantity ? values.quantity : 0

        quantity += 5
        setFieldValue('quantity', quantity)
    }
    const decrease = () => {
        let quantity = values.quantity ? values.quantity : 0
        if (!quantity || quantity == 0) {
            setFieldValue('quantity', 0)
            return
        }
        quantity -= 5
        setFieldValue('quantity', quantity)
    }

    return (
        <div>
            <Form onSubmit={handleSubmit}>
                {
                    userRole && userRole == 1 && (<FormGroup>
                        <HospitalsAsSelect handleChange={handleChange} value={values.hospitalId} />
                    </FormGroup>)
                }
                <FormGroup>
                    <BloodGroups handleChange={handleChange} value={values.bloodGroupId} />
                </FormGroup>                
                <FormGroup className="input-group input-group-lg">                   
                    <Input
                        id="quantity"
                        name="quantity"
                        placeholder="Miktar giriniz..."
                        value={values.quantity}
                        onBlur={handleBlur("quantity")}
                        onChange={handleChange}
                    ></Input>
                    <div className="btn-group">
                        <Button color='outline-danger' type="button" onClick={()=>decrease()} >-5</Button>
                        <Button color='outline-success' type="button" onClick={()=>increase()} >+5</Button>
                    </div>
                    {errors.quantity && touched.quantity && <div className="text-danger">{errors.quantity}</div>}
                </FormGroup>
                <Button type="submit" color="success" >
                    Kaydet
                </Button>
            </Form>
        </div>
    )
}

export default CreateRequestForm