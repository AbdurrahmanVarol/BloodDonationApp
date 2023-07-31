import { useFormik } from 'formik'
import React, { useContext, useEffect, useState } from 'react'
import { Button, Form, FormGroup, Input } from 'reactstrap'
import alertify from 'alertifyjs'
import axios from 'axios'
import DefaultContext from '../../contexts/DefaultContext'
import Cities from '../Cities'
import validationSchema from "./validations"
import InputMask from "react-input-mask";

const UpdateHospitalForm = ({ id }) => {

    const { token } = useContext(DefaultContext)
    const [hospital, setHospital] = useState(
        {
            id,
            name: "",
            phoneNumber: "",
            cityId: "",
            address: ""
        })

    useEffect(() => {
        axios({
            baseURL: 'https://localhost:7195/api',
            url: `/hospitals/${id}`,
            method: 'Get',
            headers: {
                "Authorization": `Bearer ${token}`
            }
        })
            .then(({ data }) => {
                setHospital({ ...data })
            })
            .catch(errors => {
                console.log(errors)
            })
    }, [])

    const { handleSubmit, handleChange, handleBlur, values, errors, touched, isSubmitting } = useFormik({
        initialValues: hospital,
        enableReinitialize: true,
        onSubmit: (values, bag) => {
            console.log(values)
            axios({
                baseURL: 'https://localhost:7195/api',
                url: '/hospitals',
                method: 'Put',
                headers: {
                    "Authorization": `Bearer ${token}`
                },
                data: values
            })
                .then(response => {
                    alertify.success('Hastane güncellendi.')
                })
                .catch(errors => {
                    alertify.error('Hastane güncellerken bir hata oluştu.')
                })
        },
        validationSchema
    })
    return (
        <div>
            <Form onSubmit={handleSubmit}>
                <FormGroup>
                    <label className="text-dark-emphasis" htmlFor="name">Hastane Adı:</label>
                    <Input
                        id="name"
                        name="name"
                        placeholder="Hastane adını giriniz..."
                        value={values.name}
                        onBlur={handleBlur("name")}
                        onChange={handleChange}
                    ></Input>
                    {errors.name && touched.name && <div className="text-danger">{errors.name}</div>}
                </FormGroup>
                <FormGroup>
                    <label className="text-dark-emphasis" htmlFor="phoneNumber">Telefon Numarası:</label>
                    <InputMask
                        id="phoneNumber"
                        name="phoneNumber"
                        className='form-control'
                        mask="+\90(999)999-99-99"
                        placeholder="+90(123)456-78-90"
                        value={values.phoneNumber}
                        onBlur={handleBlur("phoneNumber")}
                        onChange={handleChange}
                    />
                    {errors.phoneNumber && touched.phoneNumber && <div className="text-danger">{errors.phoneNumber}</div>}
                </FormGroup>
                <FormGroup>
                    <Cities handleChange={handleChange} value={values.cityId}></Cities>
                </FormGroup>
                <FormGroup>
                    <label className="text-dark-emphasis" htmlFor="address">Adres:</label>
                    <Input
                        id="address"
                        name="address"
                        type='textarea'
                        placeholder="Adres giriniz..."
                        value={values.address}
                        onBlur={handleBlur("address")}
                        onChange={handleChange}
                    ></Input>
                    {errors.address && touched.address && <div className="text-danger">{errors.address}</div>}
                </FormGroup>
                <Button type="submit" color="success" >
                    Kaydet
                </Button>
            </Form>
        </div>
    )
}

export default UpdateHospitalForm