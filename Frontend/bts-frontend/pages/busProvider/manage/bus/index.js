import { useEffect, useState } from "react"
import MyHeader from "@/pages/busProvider/component/header"
import axios from "axios"
import { info } from "autoprefixer"

const links = [
  { name: 'Open roles', href: '#' },
  { name: 'Internship program', href: '#' },
  { name: 'Our values', href: '#' },
  { name: 'Meet our leadership', href: '#' },
]
const stats = [
  { name: 'Offices worldwide', value: '12' },
  { name: 'Full-time colleagues', value: '300+' },
  { name: 'Hours per week', value: '40' },
  { name: 'Paid time off', value: 'Unlimited' },
]

export default function Example() {
  const [data, setData] = useState([])
  useEffect(()=>{
    async function fetchData(){
      try{
        const response = await axios.get(
            'https://localhost:44304/api/busProvider/bus/all',
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
          console.log(info)
        }catch{
          console.log(e)
        }
      }
    }
    fetchData();
  }, [])
  return (
    <>
    <MyHeader title="Bus Ticketing System" pagename="Bus Provider Panel: Manage Bus"></MyHeader>
    <div className="overflow-x-auto px-10">
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
    </div>
    </>
    
  )
}

