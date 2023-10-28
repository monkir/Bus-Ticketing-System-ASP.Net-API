import { useEffect, useState } from "react"
import MyHeader from "@/pages/busProvider/component/header"
import axios from "axios"
import { info } from "autoprefixer"


export default function Example() {
  const [data, setData] = useState([])
  const [message, setMessage] = useState('This is message')

  async function fetchData(searchValue=""){
    try{
      if(searchValue != ""){
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root+'/api/busProvider/bus/search/'+searchValue,
          {
              headers: {'Authorization': sessionStorage.getItem('token_string')}
          }
        )
        setData(response.data)
      }
      else{
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root+'/api/busProvider/bus/all',
          {
              headers: {'Authorization': sessionStorage.getItem('token_string')}
          }
        )
        setData(response.data)
      }
      
    }
    catch(e){
      try{
        console.log(e)
      setMessage(e.response.data.Message)
      }
      catch{
        console.log(e)
        setMessage("API is not connected")
      }
    }
  }

  useEffect(()=>{
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
    <MyHeader title="Bus Ticketing System" pagename="Bus Provider Panel: Manage Bus"></MyHeader>
    <div className="overflow-x-auto px-10 min-h-[70vh]">
      {/* Search Box */}
      <div className="grid justify-items-stretch">
        <div className=" flex justify-self-center w-1/2">
          <input
            type="text"
            name="search"
            className="block w-full rounded-md border-0 py-1.5 pl-7 pr-20 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
            placeholder="search"
            onChange={search}
          />
        </div>
      </div>
      <h1 className="justify-center"> {data.length == 0  ? "No data found": data.length +" data found "} </h1>
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
          </tr>
        </thead>
        <tbody>
          {data.map(item=>(
            <tr key={item.id}>
              <th>{item.id}</th>
              <td>{item.brand}</td>
              <td>{item.model}</td>
              <td>{item.serialNo}</td>
              <td>{item.category}</td>
              <td>{item.totalSeat}</td>
              <td>{item.bp_id}</td>
            </tr>
          ))}
          
        </tbody>
      </table>
      <p className="text-2xl text-center">{message}</p>
    </div>
    </>
    
  )
}

