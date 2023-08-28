import { useEffect, useState } from "react"
import MyHeader from "@/pages/busProvider/component/header"
import axios from "axios"

export default function Example() {
  const [data, setData] = useState([])
  useEffect(()=>{
    async function fetchData(){
      try{
        const response = await axios.get(
            'https://localhost:44304/api/busprovider/trip/all/details',
            {
                headers: {'Authorization': sessionStorage.getItem('token_string')}
            }
        )
        // console.log(response.data)
        setData(response.data)
      }
      catch(e){
        try{
          setInfo(e.response.data.Message)
        }catch{
          console.log(e)
        }
      }
    }
    fetchData();
  }, [])
  return (
    <>
    <MyHeader title="Bus Ticketing System" pagename="Bus Provider panel: Manage trip"></MyHeader>
    <div className="overflow-x-auto px-10">
      <h1> {data.length == 0 ? "No data found": ""} </h1>
      <table className="table table-zebra">
        {/* head */}
        <thead>
          <tr>
            <th>ID</th>
            <th>Ticket Price</th>
            <th>Status</th>
            <th>Start Time</th>
            <th>End Time</th>
            <th>Depot</th>
            <th>Destination</th>
          </tr>
        </thead>
        <tbody>
          {data.map(item=>(
            <tr key={item.id}>
              <th>{item.id}</th>
              <td>{item.ticketprice}</td>
              <td>{item.status}</td>
              <td>{item.startTime}</td>
              <td>{item.endTime}</td>
              <td>{item.depot?.name}</td>
              <td>{item.destination?.name}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
    </>
    
  )
}

