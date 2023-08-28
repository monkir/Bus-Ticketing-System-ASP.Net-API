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
            'https://localhost:44304/api/employee/notice/all',
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
    <MyHeader title="Bus Ticketing System" pagename="Manage Notice"></MyHeader>
    <div className="overflow-x-auto px-10">
      <h1> {data.length == 0 ? "No data found": ""} </h1>
      <table className="table table-zebra">
        {/* head */}
        <thead>
          <tr>
            <th>ID</th>
            <th>Title</th>
            <th>Description</th>
            <th>Created By</th>
          </tr>
        </thead>
        <tbody>
          {data.map(item=>(
            <tr key={item.id}>
              <th>{item.id}</th>
              <td>{item.title}</td>
              <td><pre>{item.description}</pre></td>
              <td>{item.emp_id}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
    </>
    
  )
}

