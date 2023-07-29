import React from 'react'
import { Outlet } from 'react-router-dom'

const AuthLayout = () => {
  return (
    <div className="auth">
      <div className="authComponent">
        <Outlet></Outlet>
      </div>
    </div>
  )
}

export default AuthLayout