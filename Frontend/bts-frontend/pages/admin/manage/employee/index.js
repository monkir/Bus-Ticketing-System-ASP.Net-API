import { useEffect, useState } from "react"
import MyHeader from "./../../component/header"
import axios from "axios"
import MyFooter from "../../component/footer"
import Link from "next/link"

export default function Example() {
  const [data, setData] = useState([])
  const [message, setMessage] = useState('This is message')

  async function fetchData(searchValue = "") {
    try {
      if (searchValue != "") {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/admin/employee/search/' + searchValue,
          {
            headers: { 'Authorization': sessionStorage.getItem('token_string') }
          }
        )
        setData(response.data)
      }
      else {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/admin/employee/all',
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

  const dateToShortDate = (date) => {
    const d = new Date(date);
    return d.toISOString().split('T')[0];
  }

  async function search(event) {
    // console.log(event?.target?.value)
    event.preventDefault()
    const searchValue = event?.target?.value
    fetchData(searchValue)
  }

  return (
    <>
      <MyHeader title="Bus Ticketing System" pagename="Admin Panel: Manage Employee"></MyHeader>
      <div className="overflow-x-auto px-10 min-h-[70vh]">

        <div className="grid justify-items-stretch">
          <div className=" flex justify-self-center w-1/2">
            <Link className="btn btn-active btn-outline w-1/4" href={'/admin/manage/employee/add'}>New Employee</Link>
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
        <p className="text-2xl text-red-600 text-center">{message}</p>

        <table className="table table-zebra">
          {/* head */}
          <thead>
            <tr>
              <th>ID</th>
              <th>username</th>
              <th>Name</th>
              <th>Salary</th>
              <th>DBO</th>
              <th>Created BY</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.id}>
                <th>{item.id}</th>
                <td>{item.username}</td>
                <td>{item.name}</td>
                <td>{item.salary}</td>
                <td>{dateToShortDate(item.dob)}</td>
                <td>{item.admin_id}</td>
                <td>
                  <Link className=" btn btn-info mx-1" href={"/admin/manage/employee/edit/" + item.id}>Edit</Link>
                  {/* <span className=" btn btn-warning mx-1" onClick={() => { setDeleteId(item.id); document.getElementById('my_modal_1').showModal(); }} >Delete</span> */}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <MyFooter />
    </>

  )
}

