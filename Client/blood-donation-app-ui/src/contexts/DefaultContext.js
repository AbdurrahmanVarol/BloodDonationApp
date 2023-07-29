import React, { createContext, useEffect, useState } from "react";

const DefaultContext = createContext()

export const DefaultContextProvider = ({ children }) => {

    const [token, setToken] = useState(localStorage.getItem("token"));
    const [expire, setExpire] = useState(localStorage.getItem("expire"));
    const [bloodGroup, setBloodGroup] = useState(localStorage.getItem("bloodGroup"));
    const [city, setCity] = useState(localStorage.getItem("city"));
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
        localStorage.setItem("userRole", userRole)
    }, [userRole])
    
    const values = {
        token,
        expire,
        bloodGroup,
        city,
        userRole,
        setToken,
        setExpire,
        setBloodGroup,
        setCity,
        setUserRole
    }
    return (
        <DefaultContext.Provider value={values}>{children}</DefaultContext.Provider>
    )
}

export default DefaultContext;