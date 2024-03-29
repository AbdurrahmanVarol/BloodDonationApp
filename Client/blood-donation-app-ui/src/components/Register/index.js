import { useFormik } from 'formik'
import React from 'react'
import { NavLink } from 'react-router-dom'
import { Button, Form, FormGroup, Input } from 'reactstrap'
import validationSchema from "./validations"
import BloodGroups from '../BloodGroups'
import Cities from '../Cities'
import Genders from '../Genders'
import axios from 'axios'
import alertify from 'alertifyjs'

const RegisterForm = () => {
    const { handleSubmit, handleChange, handleBlur, values, errors, touched, isSubmitting } = useFormik({
        initialValues: {
            firstName: "",
            lastName: "",
            email: "",
            cityId: "",
            genderId: "",
            bloodGroupId: "",
            userName: "",
            password: "",
            passwordConfirm: "",
        },
        onSubmit: (values, bag) => {

            axios({
                baseURL: 'https://localhost:7195/api',
                url: '/auth/register',
                method: 'post',
                data: values
            })
                .then(response => {
                    alertify.success('Kayıt olundu')
                })
                .catch(errors => {
                    console.log(errors)
                })
            console.log(values)
            bag.resetForm()
        },
        validationSchema
    })
    return (
        <div>
            <Form onSubmit={handleSubmit}>
                <FormGroup>
                    <label className="text-dark-emphasis" htmlFor="firstName">Ad:</label>
                    <Input
                        id="firstName"
                        name="firstName"
                        placeholder="Adınızı giriniz..."
                        value={values.firstName}
                        onBlur={handleBlur("firstName")}
                        onChange={handleChange}
                    ></Input>
                    {errors.firstName && touched.firstName && <div className="text-danger">{errors.firstName}</div>}
                </FormGroup>
                <FormGroup>
                    <label className="text-dark-emphasis" htmlFor="lastName">Soyad:</label>
                    <Input
                        id="lastName"
                        name="lastName"
                        placeholder="Soyadınızı giriniz..."
                        value={values.lastName}
                        onBlur={handleBlur("lastName")}
                        onChange={handleChange}
                    ></Input>
                    {errors.lastName && touched.lastName && <div className="text-danger">{errors.lastName}</div>}
                </FormGroup>
                <FormGroup>
                    <label className="text-dark-emphasis" htmlFor="email">E-posta:</label>
                    <Input
                        id="email"
                        name="email"
                        type='email'
                        placeholder="E-postanızı giriniz..."
                        value={values.email}
                        onBlur={handleBlur("email")}
                        onChange={handleChange}
                    ></Input>
                    {errors.email && touched.email && <div className="text-danger">{errors.email}</div>}
                </FormGroup>
                <FormGroup>
                    <BloodGroups handleChange={handleChange} value={values.bloodGroupId}></BloodGroups>
                </FormGroup>
                <FormGroup>
                    <Cities handleChange={handleChange} value={values.cityId}></Cities>
                </FormGroup>
                <FormGroup>
                    <Genders handleChange={handleChange} value={values.genderId}></Genders>
                </FormGroup>
                <FormGroup>
                    <label className="text-dark-emphasis" htmlFor="userName">Kullanıcı Adı:</label>
                    <Input
                        id="userName"
                        name="userName"
                        placeholder="Kullanıcı adınızı giriniz..."
                        value={values.userName}
                        onBlur={handleBlur("userName")}
                        onChange={handleChange}
                    ></Input>
                    {errors.userName && touched.userName && <div className="text-danger">{errors.userName}</div>}
                </FormGroup>
                <FormGroup>
                    <label className="text-dark-emphasis" htmlFor="password">Şifre:</label>
                    <Input
                        id="password"
                        name="password"
                        type="password"
                        placeholder="Şifrenizi giriniz..."
                        value={values.password}
                        onBlur={handleBlur("password")}
                        onChange={handleChange}
                    ></Input>
                    {errors.password && touched.password && <div className="text-danger">{errors.password}</div>}
                </FormGroup>
                <FormGroup>
                    <label className="text-dark-emphasis" htmlFor="passwordConfirm">Şifre Tekrarı:</label>
                    <Input
                        id="passwordConfirm"
                        name="passwordConfirm"
                        type="password"
                        placeholder="Şifrenizi tekrar giriniz..."
                        value={values.passwordConfirm}
                        onBlur={handleBlur("passwordConfirm")}
                        onChange={handleChange}
                    ></Input>
                    {errors.passwordConfirm && touched.passwordConfirm && <div className="text-danger">{errors.passwordConfirm}</div>}
                </FormGroup>
                <Button type="submit" color="success" disabled={isSubmitting}>
                    Kayıt Ol
                </Button>
            </Form>
            <div class="links">
                <strong class="text-dark-emphasis">Hesabınız var mı?</strong>
                <NavLink
                    to="/login"
                >
                    <strong>Giriş yap</strong>
                </NavLink>
            </div>
        </div>
    )
}

export default RegisterForm