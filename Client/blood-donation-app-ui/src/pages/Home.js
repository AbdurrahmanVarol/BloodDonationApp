import React, { useContext, useEffect, useState } from 'react'
import { Table } from 'reactstrap'
import DefaultContext from '../contexts/DefaultContext'
import axios from 'axios'

const Home = () => {
  const { token, userName, bloodGroup } = useContext(DefaultContext)
  const [requests, setRequests] = useState([])

  useEffect(() => {

    axios({
      method: "Get",
      baseURL: 'https://localhost:7195/api',
      url: `requests/bloodgroup/${bloodGroup}`,
      headers: {
        "Authorization": `Bearer ${token}`
      }
    })

      .then(response => {
        setRequests(response.data)
      })
      .catch(errors => console.log(errors))
  }, [])
  return (
    <div>
      <div className="text-center mb-3">
        <h1 className="display-4">Hoşgeldin {userName}</h1>
      </div>

      <Table borderless dark striped>
        <thead>
          <tr>
            <td>Tür</td>
            <td>Miktar</td>
            <td>Hastane Adı</td>
            <td>Şehir</td>
          </tr>
        </thead>
        <tbody>
          {
            requests.length > 0 && requests.map((request, index) => (
              <tr key={index}>
                <td>{request.bloodGroup}</td>
                <td>{request.quantity}</td>
                <td>{request.hospital}</td>
                <td>{request.city}</td>
              </tr>
            ))
          }
        </tbody>
      </Table>
    </div>
  )
}

export default Home