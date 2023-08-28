import { useEffect, useState } from "react"
import MyHeader from "@/pages/employee/component/header"
import axios from "axios"

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
            'https://localhost:44304/api/employee/busProvider/all',
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
    <MyHeader title="Bus Ticketing System" pagename="Manage Bus Provider"></MyHeader>
    <div className="overflow-x-auto px-10">
      <h1 className="justify-center"> {data.length == 0  ? "No data found": data.length +" data found "} </h1>
      <table className="table table-zebra">
        {/* head */}
        <thead>
          <tr>
            <th>ID</th>
            <th>username</th>
            <th>Company Name</th>
            <th>Added By</th>
          </tr>
        </thead>
        <tbody>
          {data.map(item=>(
            <tr key={item.id}>
              <th>{item.id}</th>
              <td>{item.username}</td>
              <td>{item.company}</td>
              <td>{item.emp_id}</td>
            </tr>
          ))}
          
        </tbody>
      </table>
    </div>
    </>
    
  )
}

