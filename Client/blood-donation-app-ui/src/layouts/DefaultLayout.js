import React, { useContext, useEffect } from 'react'
import { Outlet, useNavigate } from 'react-router-dom'
import DefaultContext from '../contexts/DefaultContext'
import Navi from '../components/Navi'
import Footer from '../components/Footer'

const DefaultLayout = () => {
  const { token, expire, clearData } = useContext(DefaultContext)
  const navigate = useNavigate()

  useEffect(() => {
    if (!token || token === "") {
      clearData();
      navigate("/login");
      return
    }
    let now = new Date().getTime();
    let expireDate = new Date(expire).getTime();

    if (now > expireDate) {
      //TODO:Refresh token eklenecek
      clearData();
      navigate("/login");
      return
    }
  }, [token, expire]);
  return (
    <div className="default">
      <Navi></Navi>
      <div className="defaultContent">
        <div className="defaultComponent pb-3 bg-light">
          <Outlet></Outlet>
        </div>
      </div>
      <Footer />
    </div>

  )
}

export default DefaultLayout