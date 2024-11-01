import { useEffect, useState } from "react"
import axios from "axios"
import { info } from "autoprefixer"
import Link from "next/link"
import BusProviderHeader from "../../component/header"
import BusProviderFooter from "../../component/footer"


export default function Example() {
  const [data, setData] = useState([])
  const [message, setMessage] = useState('This is message')

  async function fetchData(searchValue = "") {
    try {
      if (searchValue != "") {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/busProvider/bus/search/' + searchValue,
          {
            headers: { 'Authorization': sessionStorage.getItem('token_string') }
          }
        )
        setData(response.data)
      }
      else {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/busProvider/bus/all',
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
      <BusProviderHeader title="Bus Ticketing System" pagename="Bus Provider Panel: Manage Bus"></BusProviderHeader>
      <div className="overflow-x-auto px-10 min-h-[70vh]">
        <div className="grid justify-items-stretch">
          <div className=" flex justify-self-center w-1/2">
            <Link className="btn btn-active btn-outline w-1/4" href={'/busProvider/manage/bus/add'}>Add New Bus</Link>
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
              <th>Brand</th>
              <th>Model</th>
              <th>Serial No</th>
              <th>Category</th>
              <th>Total Seat</th>
              <th>Provider</th>
              <th>Provider</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.id}>
                <th>{item.id}</th>
                <td>{item.brand}</td>
                <td>{item.model}</td>
                <td>{item.serialNo}</td>
                <td>{item.category}</td>
                <td>{item.totalSeat}</td>
                <td>{item.bp_id}</td>
                <td>
                  <Link className=" btn btn-info mx-1" href={"/busProvider/manage/bus/edit/" + item.id}>Edit</Link>
                  {/* <span className=" btn btn-warning mx-1" onClick={() => { setDeleteId(item.id); document.getElementById('my_modal_1').showModal(); }} >Delete</span> */}
                </td>
              </tr>
            ))}

          </tbody>
        </table>
      </div>
      <BusProviderFooter/>
    </>

  )
}

