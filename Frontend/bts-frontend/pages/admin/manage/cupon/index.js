import { useEffect, useState } from "react"
import MyHeader from "./../../component/header"
import MyFooter from "@/pages/admin/component/footer"
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
            'https://localhost:44304/api/admin/cupon/all',
            {
                headers: {'Authorization': sessionStorage.getItem('token_string')}
            }
        )
        console.log(response.data)
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
    <MyHeader title="Bus Ticketing System" for="Manage Cupon"></MyHeader>
    <div className="overflow-x-auto px-10">
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
          {/* row 1 */}
          <tr>
            <th>1</th>
            <td>Cy Ganderton</td>
            <td>Quality Control Specialist</td>
            <td>Cy Ganderton</td>
            <td>Quality Control Specialist</td>
            <td>Blue</td>
          </tr>
          {/* row 2 */}
          <tr>
            <th>2</th>
            <td>Hart Hagerty</td>
            <td>Desktop Support Technician</td>
            <td>Hart Hagerty</td>
            <td>Desktop Support Technician</td>
            <td>Purple</td>
          </tr>
          {/* row 3 */}
          <tr>
            <th>3</th>
            <td>Brice Swyre</td>
            <td>Tax Accountant</td>
            <td>Red</td>
            <td>Tax Accountant</td>
            <td>Red</td>
          </tr>
          {/* row 3 */}
          {data.map(item=>(
            <tr>
              <th>{item.id}</th>
              <td>{item.name}</td>
              <td>{item.cupon}</td>
              <td>{item.percentage}%</td>
              <td>{item.maxDiscount}</td>
              <td>{item.admin_id}</td>
            </tr>
          ))}
          
        </tbody>
      </table>
    </div>
    {/* <MyFooter/> */}
    </>
    
  )
}

