import React, { useContext, useEffect } from 'react'
import { Outlet, useNavigate } from 'react-router-dom'
import DefaultContext from '../contexts/DefaultContext'
import Navi from '../components/Navi'

const DefaultLayout = () => {
    const {token,expire} = useContext(DefaultContext)
    const navigate = useNavigate()

    useEffect(() => {
        if (!token || token == "" ) {
          //clearData();
          navigate("/login");
          return
        }
        let now = new Date().getTime();
        let expireDate = new Date(expire).getTime();
    
        if (now > expireDate) {
          //TODO:Refresh token eklenecek
          //clearData();
          navigate("/login");
          return
        }    
      }, []);
  return (
    <div className="default">
    <div className="defaultContent">
    <Navi></Navi>
      <div className="defaultComponent container">
        <Outlet></Outlet>
      </div>
    </div>
  </div>

  )
}

export default DefaultLayout