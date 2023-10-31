import { useEffect, useState } from "react"
import EmployeeHeader from "@/pages/employee/component/header"
import axios from "axios"
import Link from "next/link"

export default function Example() {
  const [data, setData] = useState([])
  const [message, setMessage] = useState('This is message')

  async function fetchData(searchValue = "") {
    try {
      if (searchValue != "") {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/employee/busProvider/search/' + searchValue,
          {
            headers: { 'Authorization': sessionStorage.getItem('token_string') }
          }
        )
        setData(response.data)
      }
      else {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/employee/busProvider/all',
          {
            headers: { 'Authorization': sessionStorage.getItem('token_string') }
          }
        )
        setData(response.data)
      }

    }
    catch (e) {
      try {
        console.log(e)
        setMessage(e.response.data.Message)
      }
      catch {
        console.log(e)
        setMessage("API is not connected")
      }
    }
  }

  useEffect(() => {
    fetchData();
  }, [])

  async function search(event) {
    // console.log(event?.target?.value)
    event.preventDefault()
    const searchValue = event?.target?.value
    fetchData(searchValue)
  }
  return (
    <>
      <EmployeeHeader title="Bus Ticketing System" pagename="Employee: Manage Bus Provider" />
      <div className="overflow-x-auto px-10 min-h-[70vh]">
        <div className="grid justify-items-stretch">
          <div className=" flex justify-self-center w-1/2">
            <Link className="btn btn-active btn-outline w-1/4" href={'/employee/manage/busprovider/add'}>Add busProvider</Link>
            {/* Search Box */}
            <input
              type="text"
              name="search"
              className="block w-3/4 rounded-md border-0 py-1.5 pl-7 pr-20 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              placeholder="search"
              onChange={search}
            />
          </div>
        </div>
        <h1 className="justify-center"> {data.length == 0 ? "No data found" : data.length + " data found "} </h1>
        <table className="table table-zebra">
          {/* head */}
          <thead>
            <tr>
              <th>ID</th>
              <th>username</th>
              <th>Company Name</th>
              <th>Added By</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.id}>
                <th>{item.id}</th>
                <td>{item.username}</td>
                <td>{item.company}</td>
                <td>{item.emp_id}</td>
                <td>
                  <Link href={"/employee/manage/busprovider/edit/"+item.id}>Edit</Link>
                </td>
              </tr>
            ))}

          </tbody>
        </table>
        <p className="text-2xl text-center">{message}</p>
      </div>
    </>

  )
}

