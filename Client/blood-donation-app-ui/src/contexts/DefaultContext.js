import React, { createContext, useEffect, useState } from "react";

const DefaultContext = createContext()

export const DefaultContextProvider = ({ children }) => {

    const [token, setToken] = useState(localStorage.getItem("token"));
    const [expire, setExpire] = useState(localStorage.getItem("expire"));
    const [bloodGroup, setBloodGroup] = useState(localStorage.getItem("bloodGroup"));
    const [city, setCity] = useState(localStorage.getItem("city"));
    const [userName, setUserName] = useState(localStorage.getItem("userName"));
    const [userRole, setUserRole] = useState(localStorage.getItem("role"));

    useEffect(() => {
        localStorage.setItem("token", token)
    }, [token])

    useEffect(() => {
        localStorage.setItem("expire", expire)
    }, [expire])

    useEffect(() => {
        localStorage.setItem("bloodGroup", bloodGroup)
    }, [bloodGroup])

    useEffect(() => {
        localStorage.setItem("city", city)
    }, [city])

    useEffect(() => {
        localStorage.setItem("userName", userName)
    }, [userName])

    useEffect(() => {
        localStorage.setItem("userRole", userRole)
    }, [userRole])
    
    const clearData = () => {
        setToken('');
        setExpire('');
        setBloodGroup('')
        setCity('')
        setUserName('')
        setUserRole('');
      }

    const values = {
        token,
        expire,
        bloodGroup,
        city,
        userName,
        userRole,
        setToken,
        setExpire,
        setBloodGroup,
        setCity,
        setUserName,
        setUserRole,
        clearData
    }
    return (
        <DefaultContext.Provider value={values}>{children}</DefaultContext.Provider>
    )
}

export default DefaultContext;