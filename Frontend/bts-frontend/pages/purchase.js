export default function Home() {
    return <>
    {/* <button className="btn">Hello daisyUI</button>;
    <input type="checkbox" checked="checked" className="checkbox checkbox-lg" /> */}
    {(() => {
        let seat = []
        for (let i = 0; i < 10; i++) {
            seat.push(
                <>
                    {/* <label name={i} /> */}
                    <input type="checkbox" checked="checked" className="checkbox checkbox-lg" />
                    <span>{i}</span>
                    <br/>
                </>
            )
        }
        return seat
    })()}
    </>
  }