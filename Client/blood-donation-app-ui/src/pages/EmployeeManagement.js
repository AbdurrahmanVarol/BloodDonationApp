import axios from 'axios'
import React, { useContext, useEffect, useState } from 'react'
import DefaultContext from '../contexts/DefaultContext'
import { useParams } from 'react-router-dom'
import { Input } from 'reactstrap'
import alertify from 'alertifyjs'

const EmployeeManagement = () => {
    const { token } = useContext(DefaultContext)
    const [users, setUsers] = useState([])
    const [employees, setEmployees] = useState([])
    const [selectedUserId, setSelectedUserId] = useState()
    const { id } = useParams()

    const getData = async () => {
        try {
            const { data } = await axios({
                method: "Get",
                baseURL: 'https://localhost:7195/api',
                url: `/users/UsersForEmployeeManagement/${id}`,
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            })
            setUsers(data.unEmployedUsers)
            setEmployees(data.employees)
            console.log(data)
            console.log("--------------")

        }
        catch (error) {
            console.log(error)
        }

        console.log(users)
        console.log(employees)
    }

    const changeSelectedUser = event => {
        let value = event.target.value

        setSelectedUserId(value)
    }

    const addEmployee = () => {
        let data = {
            hospitalId:id,
            employeeId:selectedUserId
        }
        axios({
                method: "Post",
                baseURL: 'https://localhost:7195/api',
                url: "/hospitals/addEmployee",
                headers: {
                    "Authorization": `Bearer ${token}`
                },
                data                
            })
            .then((data) =>{
                data ? alertify.success("Personel Eklendi.") : alertify.error("Personel Eklenemedi.")
                getData()
                setSelectedUserId(0)
            })
            .catch(()=>{
                alertify.error("Personel Eklenemedi.")
            })
           
    }

    const removeEmployee = employeeId => {
        let data = {
            hospitalId:id,
            employeeId
        }
        axios({
            method: "Post",
            baseURL: 'https://localhost:7195/api',
            url: "/hospitals/removeEmployee",
            headers: {
                "Authorization": `Bearer ${token}`
            },
            data                
        })
        .then((data) =>{
            data ? alertify.success("Personel Kaldırıldı.") : alertify.error("Personel Kaldırılamadı.")
            getData()
        })
        .catch(()=>{
            alertify.error("Personel Kaldırılamadı.")
        })
        
    }

    useEffect(() => {
        getData()
    }, [])

    return (
        <div>
            <h3>Personel Ekle</h3>
            <div className="input-group input-group-lg mb-3">
                <Input type='select' id="users" onChange={changeSelectedUser}>
                    <option value="0" defaultValue hidden>Seçiniz</option>
                    {
                         users && users.length > 0 && users.map((user, index) => (
                            <option key={index} value={user.id} >{user.fullName}</option>
                         ))
                    }
                </Input>
                <div className="btn-group">
                    <button type="button" className="btn btn-outline-success" onClick={()=>addEmployee()}>Personel Ekle</button>
                </div>
            </div>

            <ul id="employeeList">
                {
                    employees && employees.length > 0 && employees.map((employee, index) => (
                        <li key={index} className="list-group-item fs-5 d-flex justify-content-between align-items-center">
                            <button className="close btn badge text-danger float-end me-2" onClick={()=>removeEmployee(employee.id)}>X</button>
                            <div className ="me-auto">
                                {employee.fullName}
                            </div>
                        </li>
                    ))
                }
            </ul>
        </div>
    )
}

export default EmployeeManagement