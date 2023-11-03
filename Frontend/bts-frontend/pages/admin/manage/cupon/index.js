import { useEffect, useState } from "react"
import MyHeader from "./../../component/header"
import MyFooter from "@/pages/admin/component/footer"
import axios from "axios"
import Link from "next/link"

export default function Example() {
  const [data, setData] = useState([])
  const [message, setMessage] = useState('This is message')

  async function fetchData(searchValue = "") {
    try {
      if (searchValue != "") {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/admin/cupon/search/' + searchValue,
          {
            headers: { 'Authorization': sessionStorage.getItem('token_string') }
          }
        )
        setData(response.data)
      }
      else {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/admin/cupon/all' + searchValue,
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
      <MyHeader title="Bus Ticketing System" pagename="Admin Panel: Manage Cupon"></MyHeader>
      <div className="overflow-x-auto px-10  min-h-[70vh]">

        <div className="grid justify-items-stretch">
          <div className=" flex justify-self-center w-1/2">
            <Link className="btn btn-active btn-outline w-1/4" href={'/admin/manage/cupon/add'}>New Cupon</Link>
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
              <th>Name</th>
              <th>Cupon</th>
              <th>Percentage</th>
              <th>Max Discount</th>
              <th>Created BY</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.id}>
                <th>{item.id}</th>
                <td>{item.name}</td>
                <td>{item.cupon}</td>
                <td>{item.percentage}%</td>
                <td>{item.maxDiscount}</td>
                <td>{item.admin_id}</td>
                <Link className="btn btn-info mx-1" href={"/admin/manage/cupon/edit/" + item.id}>Edit</Link>
              </tr>
            ))}

          </tbody>
        </table>
        <p className="text-2xl text-center">{message}</p>
      </div>
      <MyFooter />
    </>
  )
}

